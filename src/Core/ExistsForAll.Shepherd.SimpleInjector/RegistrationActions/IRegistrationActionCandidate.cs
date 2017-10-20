using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IRegistrationActionCandidate
	{
		bool ShouldRegister(ICandidateDescriptor descriptor);
		void Register(IRegistrationContext context, Container container);
	}
}