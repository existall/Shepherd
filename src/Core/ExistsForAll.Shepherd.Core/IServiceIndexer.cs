using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core
{
	public interface IServiceIndexer
	{
		FilterCollection Filters { get; }
		IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes);
	}
}