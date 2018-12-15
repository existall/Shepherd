using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	internal static class ShepherdExtension
	{
		public static Shepherd AddAssemblies(this Shepherd target,Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddAssemblies(assembly, assemblies);

			return target;
		}

		public static Shepherd AddAssembly<T>(this Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddAssemblies(assembly);

			return target;
		}

		public static Shepherd AddExportedAssemblies(this Shepherd target, Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddExportedAssemblies(assembly, assemblies);

			return target;
		}

		public static Shepherd AddExportedAssemblies<T>(this Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddExportedAssemblies(assembly);

			return target;
		}

		public static Shepherd AddModule(this Shepherd target, IModule module, params IModule[] modules)
		{
			target.Modules.AddModule(module, modules);

			return target;
		}
	}
}
