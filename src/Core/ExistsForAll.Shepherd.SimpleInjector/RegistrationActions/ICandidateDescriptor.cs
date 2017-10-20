using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface ICandidateDescriptor
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
	}

	internal struct CandidateDescriptor : ICandidateDescriptor
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }

		public CandidateDescriptor(Type serviceType, IEnumerable<Type> implementationTypes)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
		}
	}
}