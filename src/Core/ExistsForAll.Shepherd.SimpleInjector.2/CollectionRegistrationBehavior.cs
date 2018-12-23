using System.Linq;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector._2
{
	public class CollectionRegistrationBehavior : ICollectionRegistrationBehavior<Container>
	{
		public virtual bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return typeMap.ImplementationTypes.Count() > 1;
		}

		public void Register(IRegistrationContext<Container> context)
		{
			context.Container.Collection.Register(context.ServiceType, context.ImplementationTypes);
		}
	}
}