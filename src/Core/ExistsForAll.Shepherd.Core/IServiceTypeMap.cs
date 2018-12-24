using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core
{
	public interface IServiceTypeMap
	{
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
	}
}