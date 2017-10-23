using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public class GenericRegistrationBehavior : IGenericRegistrationBehavior
	{
		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return false;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{

		}
	}
}