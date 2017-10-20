using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class PublicTypesOnlyAssemblyLoader : IAssemblyLoader
	{
		public PublicTypesOnlyAssemblyLoader(Assembly assembly)
		{
			Guard.NullArgument(assembly, nameof(assembly));
			Assembly = assembly;
		}

		public Assembly Assembly { get; }

		public IEnumerable<Type> Types()
		{
			return Assembly.GetExportedTypes();
		}
	}
}