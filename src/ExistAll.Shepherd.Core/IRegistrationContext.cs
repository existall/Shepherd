using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.Shepherd.Core
{
	public interface IRegistrationContext<out T> : IRegistrationContext
	{
		T Container { get; }
	}

	public interface IRegistrationContext
	{
		IEnumerable<Type> Types { get; }
		IEnumerable<Assembly> Assemblies { get; }
	}
}