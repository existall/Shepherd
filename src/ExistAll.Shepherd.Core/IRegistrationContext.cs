using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public class SimpleInjectorServiceRegistrator 
	{
		private readonly List<Type> _initializers = new List<Type>();

		public void RegisterServices()
		{
			object castedContext = null;

			var mapper = new Dictionary<Type, List<Type>>();

			context.TypeCollection
				.Where(x => x.IsInterface)
				.Where(x => typeRegistrationFilters.All(filter => filter.OnTypeEnumeration(new EnumerationContext(x))))
				.ForEach(x => mapper.Add(x, new List<Type>()));

			context.TypeCollection.Where(x => !x.IsInterface && !x.IsAbstract)
				.ForEach(typeCandidate => {
					var interfaces = typeCandidate.GetInterfaces().ToArray();

					if (!interfaces.Any())
						return;

					foreach (var @interface in interfaces)
					{
						if (mapper.ContainsKey(@interface))
							mapper[@interface].Add(typeCandidate);
					}
				});

			var registrations = mapper.Where(x => typeRegistrationFilters
			.All(filter => filter.OnTypeEnumeration(new EnumerationContext(x.Key))))
				.Select(i => new Registration(i.Key, i.Value.ToArray()))
				.ToArray();

			foreach (var reg in registrations)
			{
				HandleRegistration(reg, container, castedContext.Assemblies);
			}

			container.Register(typeof(IInitializerTypeProvider), () => new InitializerTypeProvider(_initializers));
		}

		private void HandleRegistration(Registration reg, Container container, Assembly[] assemblies)
		{
			if (!reg.ShouldRegister)
				return;

			if (reg.IsGeneric)
			{
				RegisterGeneric(reg, container, assemblies);
			}

			else if (reg.IsArray)
			{
				RegisterCollection(reg, container);
			}
			else if (reg.IsDecorator)
			{
				RegisterDecoration(reg, container);

			}
			else
			{
				RegisterService(reg, container);
			}
		}

		private void RegisterService(Registration reg, Container container)
		{
			container.Register(reg.Interface, reg.Implementors.First(), Lifestyle.Singleton);

			if (reg.Implementors.First().HasInterface(typeof(IComponentInitializer)))
				_initializers.Add(reg.Interface);
		}

		private void RegisterGeneric(Registration reg, Container container, Assembly[] assemblies)
		{
			var supportedtypes = container.GetTypesToRegister(reg.Interface, assemblies, new TypesToRegisterOptions
			{
				IncludeGenericTypeDefinitions = true
			}).ToArray();

			if (reg.Interface.HasAttribute<RegisterAllImplementationsAttribute>())
			{
				container.RegisterCollection(reg.Interface, supportedtypes.Select(x => Lifestyle.Singleton.CreateRegistration(x, container)));
			}

			foreach (var imp in supportedtypes)
			{
				container.RegisterSingleton(reg.Interface, imp);
			}
		}

		private void RegisterDecoration(Registration reg, Container container)
		{
			if (reg.Implementors.Length != 2)
			{
				var sb = new StringBuilder();
				sb.AppendLine("Decoration can only be with with one service and one decorator");
				sb.AppendLine($"for interface [{reg.Interface.Name}] you have [{reg.Implementors.Length}] implementors.");

				foreach (var imp in reg.Implementors)
				{
					sb.AppendLine($"implementor [{imp.Name}]");
				}

				throw new Exception(sb.ToString());
			}


			var decorator = reg.Implementors.First(x => x.Name.EndsWith("Decorator"));

			var composeService = reg.Implementors.First(x => !x.Name.EndsWith("Decorator"));

			container.Register(reg.Interface, composeService);
			container.RegisterDecorator(reg.Interface, decorator);
		}

		private void RegisterCollection(Registration registration, Container container)
		{
			container.RegisterCollection(registration.Interface,
					registration.Implementors.Select(x => Lifestyle.Singleton.CreateRegistration(x, container)));

			container.RegisterSingleton(registration.Interface.MakeArrayType(), () =>
			{
				var array = container.GetAllInstances(registration.Interface).ToArray();

				var registeredArray = Array.CreateInstance(registration.Interface, array.Length);

				array.CopyTo(registeredArray, 0);

				return registeredArray;
			});
		}
	}

}