using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.Shepherd.Core
{
	public interface IAssemblyLoader
	{
		Assembly Assembly { get; }
		IEnumerable<Type> Types();
	}
}