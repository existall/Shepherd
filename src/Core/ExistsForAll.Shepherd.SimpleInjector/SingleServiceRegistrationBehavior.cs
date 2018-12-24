using System.Linq;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class SingleServiceRegistrationBehavior : ISingleServiceRegistrationBehavior<Container>
	{
		public virtual bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return typeMap.ImplementationTypes.Count() == 1;
		}

		public void Register(IRegistrationContext<Container> context)
		{
			context.Container.Register(context.ServiceType, context.ImplementationTypes.First());	
		}
	}
}