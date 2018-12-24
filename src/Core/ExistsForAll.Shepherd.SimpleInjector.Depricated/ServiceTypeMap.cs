using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public struct ServiceTypeMap
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }

		public ServiceTypeMap(Type serviceType, IEnumerable<Type> implementationTypes)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
		}
	}
}