using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using ExistAll.Shepherd.Core.Resources;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IRegistrationActionCandidate
	{
		bool ShouldRegister(ICandidateDescriptor descriptor);
		void Register(IRegistrationContext context, Container container);
	}

	public interface ICandidateDescriptor
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
	}

	public interface IRegistrationContext
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
		IEnumerable<Assembly> Assemblies { get; }
	}


	public class SkipRegistrationAttribute : Attribute
	{ }

	public interface ISkipRegistration : IRegistrationActionCandidate
	{ }

	public interface IGenericRegistration : IRegistrationActionCandidate
	{ }

	public class SkipRegistrationAction : ISkipRegistration
	{
		public Type AttributeType { get; set; } = typeof(SkipRegistrationAttribute);

		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			if (AttributeType == null)
				throw new AutoRegistrationException(ExceptionMessages.SkipRegistrationMessage);

			return descriptor.ServiceType.GetCustomAttribute(AttributeType) != null;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
		}
	}

	public class GenericRegistration : IGenericRegistration
	{
		public bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			throw new NotImplementedException();
		}

		public void Register(IRegistrationContext context, Container container)
		{
			throw new NotImplementedException();
		}
	}


	public class SimpleInjectorServiceRegistrator
	{



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