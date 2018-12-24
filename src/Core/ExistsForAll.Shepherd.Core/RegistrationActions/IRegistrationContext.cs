using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	public interface IRegistrationContext
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
		IEnumerable<Assembly> Assemblies { get; }
		Dictionary<string, object> Properties { get; }
	}
	
	public interface IRegistrationContext<out TContainer> : IRegistrationContext
	{
		TContainer Container { get; }
	}
}