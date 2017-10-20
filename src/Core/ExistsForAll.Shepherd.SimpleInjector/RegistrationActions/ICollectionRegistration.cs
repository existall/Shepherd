using System.Linq;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface ICollectionRegistration : IRegistrationActionCandidate { }

	public class CollectionRegistration : ICollectionRegistration
	{
		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			return descriptor.ImplementationTypes.Count() > 1;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			container.RegisterCollection(context.ServiceType, context.ImplementationTypes);
		}
	}
}