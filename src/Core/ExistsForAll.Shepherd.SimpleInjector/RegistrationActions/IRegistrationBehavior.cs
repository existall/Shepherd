using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IRegistrationBehavior
	{
		bool ShouldRegister(IServiceDescriptor descriptor);
		void Register(IRegistrationContext context, Container container);
	}
}