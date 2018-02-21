using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class Shepherd
	{
		private readonly IOptionsValidator _optionsValidator;
		private readonly IModulesExecutor _modulesExecutor;
		private readonly IAutoRegistrationBehavior _autoRegistrationBehavior;

		private Container Container { get; }

		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new ShepherdOptions();

		public Shepherd(Container container = null)
			: this(new OptionsValidator(),
				  new ModuleExecutor(),
				  new AutoRegistrationBehavior())
		{
			Container = container ?? new Container();
		}

		internal Shepherd(IOptionsValidator optionsValidator,
			IModulesExecutor modulesExecutor,
			IAutoRegistrationBehavior autoRegistrationBehavior)
		{
			_optionsValidator = optionsValidator;
			_modulesExecutor = modulesExecutor;
			_autoRegistrationBehavior = autoRegistrationBehavior;
		}

		public Container Herd()
		{
			X();

			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var assemblies = Assemblies.Assemblies
				.ToArray();

			var typeIndex = Options.ServiceIndexer.MapTypes(allTypes);

			_autoRegistrationBehavior.Register(Container, Options, typeIndex, assemblies);

			_modulesExecutor.ExecuteModules(Modules, Container, assemblies, allTypes);

			return Container;
		}

		private void X()
		{
			Container.ResolveUnregisteredType += (s, e) =>
			{
				if (e.Handled)
					return;

				var type = e.UnregisteredServiceType;
				var typeInfo = type.GetTypeInfo();

				//var isAssignableFrom = typeof(IEnumerable).IsAssignableFrom(type);

				Type t = null;

				if (type.IsArray)
				{
					t = type.GetElementType();
				}

				if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					t = typeInfo.GetGenericArguments().Single();
				}

				InstanceProducer producers = Container.GetRegistration(t);

				// Create a stream --> array should be handled differntly !!!!
				var castMethod = typeof(Enumerable).GetTypeInfo().GetMethod("Cast").MakeGenericMethod(t);
				object stream = new [] {producers.GetInstance() }.Select(x => x);
				stream = castMethod.Invoke(null, new[] { stream });

				// Register stream as singleton
				e.Register(Lifestyle.Singleton.CreateRegistration(type, () => stream, Container));


				if (type.IsArray)
				{
					 t = type.GetElementType();
				}
				else
				{
					Type[] interfaces = type.GetTypeInfo().GetInterfaces();

					t = interfaces.Where(x => x.IsGenericType() && x.GetGenericTypeDefinition() == typeof(IEnumerable<>))
						.Select(x => x.GenericTypeArguments[0])
						.FirstOrDefault();
				}

				//var instanceProducer = Container.GetRegistration(t);
			};
		}
	}
}
