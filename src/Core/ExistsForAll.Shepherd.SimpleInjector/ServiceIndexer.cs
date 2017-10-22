using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IServiceIndexer
	{
		Predicate<Type> TypeFilter { get; set; }
		IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes);
	}
}