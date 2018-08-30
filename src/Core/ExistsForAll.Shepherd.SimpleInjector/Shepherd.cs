using System.Linq;
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

		public Shepherd(Container container)
			: this(new OptionsValidator(),
				new ModuleExecutor(),
				new AutoRegistrationBehavior())
		{
			Container = container;
		}

		internal Shepherd(IOptionsValidator optionsValidator,
			IModulesExecutor modulesExecutor,
			IAutoRegistrationBehavior autoRegistrationBehavior)
		{
			_optionsValidator = optionsValidator;
			_modulesExecutor = modulesExecutor;
			_autoRegistrationBehavior = autoRegistrationBehavior;
		}

		public void Herd()
		{
			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			Container.AddSingleAsCollectionSupport();
			
			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var assemblies = Assemblies.Assemblies
				.ToArray();

			var typeIndex = Options.ServiceIndexer.MapTypes(allTypes);

			_autoRegistrationBehavior.Register(Container, Options, typeIndex, assemblies);

			_modulesExecutor.ExecuteModules(Modules, Container, assemblies, allTypes);
		}
	}
}