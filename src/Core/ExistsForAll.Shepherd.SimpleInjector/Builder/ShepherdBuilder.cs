using System;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Builder
{
	public static class ShepherdBuilderExtensions
	{
		public static ShepherdBuilder WithAssembly<T>(this ShepherdBuilder target)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			target.WithAssemblies(x => x.AddAssemblies(assembly));

			return target;
		}

		public static ShepherdBuilder WithExportedAssembly<T>(this ShepherdBuilder target)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			target.WithAssemblies(x => x.AddExportedAssemblies(assembly));

			return target;
		}

		public static ShepherdBuilder UseServiceIndexer(this ShepherdBuilder target, IServiceIndexer serviceIndexer)
		{
			target.WithOptions(x => x.ServiceIndexer = serviceIndexer);

			return target;
		}
	}

	public class ShepherdBuilder
	{
		private Shepherd Shepherd { get; }

		public ShepherdBuilder(Shepherd shepherd)
		{
			Shepherd = shepherd;
		}

		public ShepherdBuilder WithOptions(Action<IShepherdOptions> action)
		{
			action?.Invoke(Shepherd.Options);
			return this;
		}

		public ShepherdBuilder WithAssemblies(Action<AssemblyCollection> action)
		{
			action?.Invoke(Shepherd.Assemblies);
			return this;
		}

		public ShepherdBuilder WithModules(Action<ModuleCollection> action)
		{
			action?.Invoke(Shepherd.Modules);
			return this;
		}
	}
}
