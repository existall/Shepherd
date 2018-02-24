using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	public static class ShepherdExtension
	{
		public static Shepherd AddCompleteTypeAssemblies(this Shepherd target,Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddCompleteTypeAssemblies(assembly, assemblies);

			return target;
		}

		public static Shepherd AddCompleteTypeAssemby<T>(this Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddCompleteTypeAssemblies(assembly);

			return target;
		}

		public static Shepherd AddPublicTypesAssemblies(this Shepherd target, Assembly assembly, params Assembly[] assemblies)
		{
			target.Assemblies.AddPublicTypesAssemblies(assembly, assemblies);

			return target;
		}

		public static Shepherd AddPublicTypesAssemblies<T>(this Shepherd target)
		{
			var assembly = typeof(T).Info().Assembly;

			target.Assemblies.AddPublicTypesAssemblies(assembly);

			return target;
		}

		public static Shepherd AddModule(this Shepherd target, IModule module, params IModule[] modules)
		{
			target.Modules.AddModule(module, modules);

			return target;
		}
	}
}
