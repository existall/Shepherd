namespace ExistAll.Shepherd.Core
{
	public class Shepherd : IShepherd
	{
		public AssemblyCollection Assemblies { get; internal set; } = new AssemblyCollection();
		public ModuleCollection Modules { get; internal set; } = new ModuleCollection();
		public IShepherdOptions Options { get; internal set; } = new DefaultShepherdOptions();

		internal Shepherd()
		{
			
		}

		public IServiceResolver Herd()
		{
			var allTypes = Assemblies.GetAllTypes();

			Options.

			return null;
		}
	}

	internal interface IOptionsValidator
	{
		
	}
}
