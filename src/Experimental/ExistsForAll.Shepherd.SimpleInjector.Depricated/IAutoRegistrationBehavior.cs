using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal interface IAutoRegistrationBehavior
	{
		void Register(Container container,
			IShepherdOptions options,
			IEnumerable<ServiceTypeMap> typeIndex,
			Assembly[] assemblies);
	}
}