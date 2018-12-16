using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IRegistrationConstraintBehavior
	{
		bool ShouldSkipAutoRegistration(IServiceTypeMap typeMap);
	}
}