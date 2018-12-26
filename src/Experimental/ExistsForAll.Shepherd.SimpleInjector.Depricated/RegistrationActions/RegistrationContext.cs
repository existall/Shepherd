using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions
{
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