namespace ExistAll.Shepherd.Core
{
	public class Shepherd : IShepherd
	{
		private readonly IShepherdOptions _shepherdOptions;

		internal Shepherd(IShepherdOptions shepherdOptions)
		{
			_shepherdOptions = shepherdOptions;
		}

		public IServiceResolver Herd(AssemblyCollection assemblies, ModuleCollection collection)
		{
			return null;
		}
	}
}