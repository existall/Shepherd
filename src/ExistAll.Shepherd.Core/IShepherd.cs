namespace ExistAll.Shepherd.Core
{
	public interface IShepherd
	{
		IServiceResolver Herd(AssemblyCollection assemblies, ModuleCollection collection);
	}
}