using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions
{
	internal static class ShepherdExtension
	{
		public static ExistsForAll.Shepherd AddAssemblies(this ExistsForAll.Shepherd target,Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddAssemblies(assembly, assemblies);

			return target;
		}

		public static ExistsForAll.Shepherd AddAssembly<T>(this ExistsForAll.Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddAssemblies(assembly);

			return target;
		}

		public static ExistsForAll.Shepherd AddExportedAssemblies(this ExistsForAll.Shepherd target, Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddExportedAssemblies(assembly, assemblies);

			return target;
		}

		public static ExistsForAll.Shepherd AddExportedAssemblies<T>(this ExistsForAll.Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddExportedAssemblies(assembly);

			return target;
		}

		public static ExistsForAll.Shepherd AddModule(this ExistsForAll.Shepherd target, IModule module, params IModule[] modules)
		{
			target.Modules.AddModule(module, modules);

			return target;
		}
	}
}
