﻿using System.Linq;
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

			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var assemblies = Assemblies.Assemblies
				.ToArray();

			_modulesExecutor.ExecuteModules(Modules, Container, assemblies, allTypes);

			var typeIndex = Options.ServiceIndexer.MapTypes(allTypes);

			_autoRegistrationBehavior.Register(Container, Options, typeIndex, assemblies);

			return Container;
		}
	}
}
