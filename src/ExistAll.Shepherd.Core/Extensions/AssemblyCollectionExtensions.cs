using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.Shepherd.Core.Extensions
{
	public static class AssemblyCollectionExtensions
	{
		public static AssemblyCollection AddPublicTypesAssemblies(this AssemblyCollection target, params Assembly[] assemblies)
		{
			AddAssemblies(target, assemblies, x => new PublicTypesOnlyAssemblyLoader(x));

			return target;
		}

		public static AssemblyCollection AddAllTypeAssemblies(this AssemblyCollection target, params Assembly[] assemblies)
		{
			AddAssemblies(target, assemblies, x => new AllTypesAssemblyLoader(x));

			return target;
		}

		private static void AddAssemblies(AssemblyCollection assemblyCollection, Assembly[] assemblies, Func<Assembly, IAssemblyLoader> action)
		{
			Guard.NullArgument(assemblyCollection, nameof(assemblyCollection));

			var publicTypesOnlyAssemblyLoaders = assemblies.Select(action).ToArray();

			assemblyCollection.AddRange(publicTypesOnlyAssemblyLoaders);
		}
	}
}
