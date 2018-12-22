using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal class ModuleContext<TContainer> : IModuleContext<TContainer>
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }
		public TContainer Container { get; }

		public ModuleContext(IEnumerable<Assembly> assemblies,
			IEnumerable<Type> types,
			TContainer container)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
		}
	}
}