using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public class CompleteTypeAssemblyLoader : IAssemblyLoader
	{
		public CompleteTypeAssemblyLoader(Assembly assembly)
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