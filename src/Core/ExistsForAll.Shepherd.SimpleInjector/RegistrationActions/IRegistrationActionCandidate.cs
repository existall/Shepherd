using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IRegistrationActionCandidate
	{
		bool ShouldRegister(IServiceDescriptor descriptor);
		void Register(IRegistrationContext context, Container container);
	}
}