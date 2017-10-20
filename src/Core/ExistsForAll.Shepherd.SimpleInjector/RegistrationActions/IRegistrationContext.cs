using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IRegistrationContext
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
		IEnumerable<Assembly> Assemblies { get; }
	}

	internal struct RegistrationContext : IRegistrationContext
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }
		public IEnumerable<Assembly> Assemblies { get; }

		public RegistrationContext(Type serviceType, IEnumerable<Type> implementationTypes, IEnumerable<Assembly> assemblies)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
			Assemblies = assemblies;
		}
	}
}