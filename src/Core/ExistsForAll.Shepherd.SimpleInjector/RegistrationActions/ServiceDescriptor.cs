using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	internal struct ServiceDescriptor : IServiceDescriptor
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }

		public ServiceDescriptor(Type serviceType, IEnumerable<Type> implementationTypes)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
		}
	}
}