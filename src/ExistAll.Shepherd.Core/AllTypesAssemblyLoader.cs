using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.Shepherd.Core
{
	public class AllTypesAssemblyLoader : IAssemblyLoader
	{
		public AllTypesAssemblyLoader(Assembly assembly)
		{
			Guard.NullArgument(assembly, nameof(assembly));
			Assembly = assembly;
		}

		public Assembly Assembly { get; }

		public IEnumerable<Type> Types()
		{
			return Assembly.GetTypes();
		}
	}
}