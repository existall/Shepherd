using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal interface IAutoRegistrationBehavior<TContainer>
	{
		void Register(TContainer container,
			IShepherdOptions<TContainer> options,
			IEnumerable<ServiceTypeMap> typeIndex,
			Assembly[] assemblies);
	}
}