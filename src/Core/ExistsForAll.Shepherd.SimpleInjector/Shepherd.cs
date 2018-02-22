using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;
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
			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			RegisterSingleToCollectionEvent(Container);
			
			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var assemblies = Assemblies.Assemblies
				.ToArray();

			var typeIndex = Options.ServiceIndexer.MapTypes(allTypes);

			_autoRegistrationBehavior.Register(Container, Options, typeIndex, assemblies);

			_modulesExecutor.ExecuteModules(Modules, Container, assemblies, allTypes);

			return Container;
		}

		private static void RegisterSingleToCollectionEvent(Container conatiner)
		{
			conatiner.ResolveUnregisteredType += (s, e) =>
			{
				if (e.Handled)
					return;

				var serviceType = e.UnregisteredServiceType;

				if (serviceType.IsArray)
				{
					throw new AutoRegistrationException(ExceptionMessages.AutoRegisterArrayExceptionMessage(serviceType.GetElementType()));
				}
				
				if (!serviceType.IsGenericType() || serviceType.GetGenericTypeDefinition() != typeof(IEnumerable<>)) 
					return;
				
				var elementType = serviceType.GetGenericArguments().Single();
				var producer = conatiner.GetRegistration(elementType);
				
				if(producer == null)
					throw new AutoRegistrationException(ExceptionMessages.AutoRegisterCollectionExceptionMessage(elementType));
				
				// Create a stream --> array should be handled differntly !!!!

				var castMethod = typeof(Enumerable)
					.GetTypeInfo()
					.GetMethod("Cast")
					.MakeGenericMethod(elementType);
					
				object stream = new[] {producer.GetInstance()}.Select(x => x);
					
				stream = castMethod.Invoke(null, new[] {stream});

				e.Register(producer.Lifestyle.CreateRegistration(serviceType, () => stream, conatiner));
			};
		}
	}
}