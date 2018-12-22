using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	public class AssemblyLoader : IAssemblyLoader
	{
		public AssemblyLoader(Assembly assembly)
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