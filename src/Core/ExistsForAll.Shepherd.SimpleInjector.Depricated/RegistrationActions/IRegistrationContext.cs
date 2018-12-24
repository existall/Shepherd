using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions
{
	public interface IRegistrationContext
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
		IEnumerable<Assembly> Assemblies { get; }
	}
}