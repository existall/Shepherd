using System.Linq;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public class SingleServiceRegistrationBehavior : ISingleServiceRegistrationBehavior
	{
		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return descriptor.ImplementationTypes.Count() == 1;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			container.Register(context.ServiceType,context.ImplementationTypes.First());
		}
	}
}