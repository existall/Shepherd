using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IShepherd
	{
		AssemblyCollection Assemblies { get; }
		ModuleCollection Modules { get; }
		IShepherdOptions Options { get; }
		Container Herd();
	}
}