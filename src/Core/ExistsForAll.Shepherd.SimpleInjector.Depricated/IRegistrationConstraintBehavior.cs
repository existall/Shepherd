using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public interface IRegistrationConstraintBehavior
	{
		bool ShouldSkipAutoRegistration(IServiceTypeMap typeMap);
	}
}