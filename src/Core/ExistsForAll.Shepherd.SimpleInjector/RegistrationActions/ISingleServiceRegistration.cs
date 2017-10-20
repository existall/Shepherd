using System.Linq;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface ISingleServiceRegistration : IRegistrationActionCandidate { }

	public class SingleServiceRegistration : ISingleServiceRegistration
	{
		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			return descriptor.ImplementationTypes.Count() == 1;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			container.Register(context.ServiceType,context.ImplementationTypes.First());
		}
	}
}