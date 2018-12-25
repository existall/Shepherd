using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public interface IAssemblyLoader
	{
		Assembly Assembly { get; }
		IEnumerable<Type> Types();
	}
}