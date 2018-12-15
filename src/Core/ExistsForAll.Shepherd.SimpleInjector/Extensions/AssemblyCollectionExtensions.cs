using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	public static class AssemblyCollectionExtensions
	{
		public static AssemblyCollection AddExportedAssemblies(this AssemblyCollection target, Assembly assembly, params Assembly[] assemblies)
		{
			AddAssemblies(target, x => new PublicTypesAssemblyLoader(x), JoinAssemblies(assembly, assemblies));

			return target;
		}

		public static AssemblyCollection AddAssemblies(this AssemblyCollection target, Assembly assembly, params Assembly[] assemblies)
		{
			AddAssemblies(target, x => new CompleteTypeAssemblyLoader(x), JoinAssemblies(assembly, assemblies));

			return target;
		}

		private static IEnumerable<Assembly> JoinAssemblies(Assembly assembly, params Assembly[] assemblies)
		{
			return new List<Assembly>(assemblies)
			{
				assembly
			};
		}

		private static void AddAssemblies(AssemblyCollection assemblyCollection, Func<Assembly, IAssemblyLoader> action, IEnumerable<Assembly> assemblies)
		{
			Guard.NullArgument(assemblyCollection, nameof(assemblyCollection));

			var publicTypesOnlyAssemblyLoaders = assemblies.Select(action).ToArray();

			assemblyCollection.AddRange(publicTypesOnlyAssemblyLoaders);
		}
	}
}