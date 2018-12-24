using System.Linq;

namespace ExistsForAll.Shepherd.Core
{
	public abstract class Shepherd<TContainer>
	{
		private readonly IOptionsValidator<TContainer> _optionsValidator;
		private readonly IModulesExecutor<TContainer> _modulesExecutor;
		private readonly IAutoRegistrationBehavior<TContainer> _autoRegistrationBehavior;

		private TContainer Container { get; }

		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection<TContainer> Modules { get; internal set; } = new ModuleCollection<TContainer>();
		public abstract IShepherdOptions<TContainer> Options { get; protected set; }

		protected Shepherd(TContainer container)
			: this(new OptionsValidator<TContainer>(),
				new ModuleExecutor<TContainer>(),
				new AutoRegistrationBehavior<TContainer>())
		{
			Container = container;
		}

		internal Shepherd(IOptionsValidator<TContainer> optionsValidator,
			IModulesExecutor<TContainer> modulesExecutor,
			IAutoRegistrationBehavior<TContainer> autoRegistrationBehavior)
		{
			_optionsValidator = optionsValidator;
			_modulesExecutor = modulesExecutor;
			_autoRegistrationBehavior = autoRegistrationBehavior;
		}

		public void Herd()
		{
			_optionsValidator.ValidateOptions(Options);

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