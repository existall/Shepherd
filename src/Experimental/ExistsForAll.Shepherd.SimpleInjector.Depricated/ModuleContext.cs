using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal class ModuleContext : IModuleContext
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }
		public Container Container { get; }

		public ModuleContext(IEnumerable<Assembly> assemblies,
			IEnumerable<Type> types,
			Container container)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
		}
	}
}