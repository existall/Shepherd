using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public class Shepherd : IShepherd
	{
		private readonly IOptionsValidator _optionsValidator;
		private readonly IModulesExecutor _modulesExecutor;

		private Container Container { get; }

		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new ShepherdOptions();

		public Shepherd(Container container = null)
			: this(new OptionsValidator(), new ModuleExecutor())
		{
			if (container == null)
				Container = new Container();
		}

		internal Shepherd(IOptionsValidator optionsValidator,
			IModulesExecutor modulesExecutor)
		{
			_optionsValidator = optionsValidator;
			_modulesExecutor = modulesExecutor;
		}

		public Container Herd()
		{
			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			_modulesExecutor.ExecuteModules(Modules, Container, Assemblies.Assemblies.ToArray(), allTypes);

			var typeIndex = GetTypeIndex(allTypes);

			return null;
		}

		private IEnumerable<KeyValuePair<Type, IEnumerable<Type>>> GetTypeIndex(Type[] allTypes)
		{
			var filterTypes = Options.TypeMatcher.FilterTypes(allTypes);

			var typesIndex = Options.TypeMatcher.MapTypes(filterTypes);

			return typesIndex;
		}
	}
}
