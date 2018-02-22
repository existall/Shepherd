using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IRegistrationConstraintBehavior
	{
		bool ShouldSkipAutoRegistration(IServiceDescriptor descriptor);
	}
}