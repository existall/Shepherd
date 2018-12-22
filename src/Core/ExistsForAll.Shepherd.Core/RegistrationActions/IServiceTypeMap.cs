using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	public interface IServiceTypeMap
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
	}
}