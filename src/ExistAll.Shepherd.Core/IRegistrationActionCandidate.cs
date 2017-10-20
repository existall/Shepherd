using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IRegistrationActionCandidate
	{
		bool ShouldRegister(ICandidateDescriptor descriptor);
		void Register(IRegistrationContext context, Container container);
	}
}