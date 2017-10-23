using System.Linq;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface ICollectionRegistrationBehavior : IRegistrationBehavior { }

	public class CollectionRegistrationBehavior : ICollectionRegistrationBehavior
	{
		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return descriptor.ImplementationTypes.Count() > 1;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			container.RegisterCollection(context.ServiceType, context.ImplementationTypes);
		}
	}
}