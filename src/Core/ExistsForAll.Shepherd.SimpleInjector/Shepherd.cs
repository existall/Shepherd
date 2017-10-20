using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class Shepherd : IShepherd
	{
		private readonly IOptionsValidator _optionsValidator;
		private readonly IModulesExecutor _modulesExecutor;
		private readonly IDeepServiceRegistrator _deepServiceRegistrator;

		private Container Container { get; }

		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new ShepherdOptions();

		public Shepherd(Container container = null)
			: this(new OptionsValidator(),
				  new ModuleExecutor(),
				  new DeepServiceRegistrator())
		{
			if (container == null)
				Container = new Container();
		}

		internal Shepherd(IOptionsValidator optionsValidator,
			IModulesExecutor modulesExecutor,
			IDeepServiceRegistrator deepServiceRegistrator)
		{
			_optionsValidator = optionsValidator;
			_modulesExecutor = modulesExecutor;
			_deepServiceRegistrator = deepServiceRegistrator;
		}

		public Container Herd()
		{
			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var assemblies = Assemblies.Assemblies.ToArray();

			_modulesExecutor.ExecuteModules(Modules, Container, assemblies, allTypes);

			var typeIndex = GetTypeIndex(allTypes);

			_deepServiceRegistrator.Register(Container, Options, typeIndex, assemblies);

			return Container;
		}

		private IEnumerable<KeyValuePair<Type, IEnumerable<Type>>> GetTypeIndex(Type[] allTypes)
		{
			var filterTypes = Options.TypeMatcher.FilterTypes(allTypes);

			var typesIndex = Options.TypeMatcher.MapTypes(filterTypes);

			return typesIndex;
		}
	}
}
