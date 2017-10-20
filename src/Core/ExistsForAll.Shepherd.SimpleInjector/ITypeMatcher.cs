using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface ITypeMatcher
	{
		IEnumerable<Type> FilterTypes(IEnumerable<Type> applicationTypes);
		IEnumerable<KeyValuePair<Type, IEnumerable<Type>>> MapTypes(IEnumerable<Type> applicationTypes);
	}
}