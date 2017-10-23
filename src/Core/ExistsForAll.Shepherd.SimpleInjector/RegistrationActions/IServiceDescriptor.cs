using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IServiceDescriptor
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
	}
}