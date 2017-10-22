using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	public static class ShepherdExtension
	{
		public static IShepherd AddCompleteTypeAssembly(this IShepherd target,Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddCompleteTypeAssembly(assembly, assemblies);

			return target;
		}

		public static IShepherd AddPublicTypesAssemblies(this IShepherd target, Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddPublicTypesAssemblies(assembly, assemblies);

			return target;
		}

		public static IShepherd AddModule(this IShepherd target, IModule module, params IModule[] modules)
		{
			target.Modules.AddModule(module, modules);

			return target;
		}
	}
}
