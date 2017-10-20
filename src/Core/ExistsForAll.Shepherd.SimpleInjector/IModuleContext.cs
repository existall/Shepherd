using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IModuleContext
	{
		IEnumerable<Assembly> Assemblies { get; }
		IEnumerable<Type> Types { get; }
		Container Container { get; }
	}
}