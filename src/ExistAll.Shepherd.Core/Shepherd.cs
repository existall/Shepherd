using System.Linq;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public class Shepherd : IShepherd
	{
		private readonly IOptionsValidator _optionsValidator;
		private Container Container { get; }

		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new ShepherdOptions();

		public Shepherd(Container container = null)
			: this(new OptionsValidator())
		{
			if(container == null)
				Container = new Container();
		}

		internal Shepherd(IOptionsValidator optionsValidator)
		{
			_optionsValidator = optionsValidator;
		}

		public IServiceResolver Herd()
		{
			_optionsValidator.ValidateOptions(Options);

			Options?.ConfigureContainerOptions.Configure(Container.Options);

			var allTypes = Assemblies.GetAllTypes()
				.ToArray();

			var context = new ModuleContext(Assemblies.Assemblies, allTypes, Container);

			// run all modules here.!

			var filterTypes = Options.TypeMatcher.FilterTypes(allTypes);

			var typesIndex = Options.TypeMatcher.MapTypes(filterTypes);

			return null;
		}
	}
}
