using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	public interface IModuleContext<out TContainer>
	{
		IEnumerable<Assembly> Assemblies { get; }
		IEnumerable<Type> Types { get; }
		TContainer Container { get; }
	}
}