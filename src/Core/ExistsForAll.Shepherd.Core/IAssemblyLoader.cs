using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	public interface IAssemblyLoader
	{
		Assembly Assembly { get; }
		IEnumerable<Type> Types();
	}
}