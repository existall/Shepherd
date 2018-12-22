using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public interface IRegistrationConstraintBehavior
	{
		bool ShouldSkipAutoRegistration(IServiceTypeMap typeMap);
	}
}