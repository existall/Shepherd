namespace ExistAll.Shepherd.Core
{
	public class Shepherd : IShepherd
	{
		private readonly IOptionsValidator _optionsValidator;
		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new ShepherdOptions();

		public Shepherd()
			: this(new OptionsValidator())
		{
			
		}

		internal Shepherd(IOptionsValidator optionsValidator)
		{
			_optionsValidator = optionsValidator;
		}

		public IServiceResolver Herd()
		{
			_optionsValidator.ValidateOptions(Options);

			var allTypes = Assemblies.GetAllTypes();

			var context = new ModuleContext();

			var filterTypes = Options.TypeMatcher.FilterTypes(allTypes);

			var typesIndex = Options.TypeMatcher.MapTypes(filterTypes);

			return null;
		}
	}
}
