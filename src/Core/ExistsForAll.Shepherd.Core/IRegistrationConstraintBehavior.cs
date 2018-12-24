namespace ExistsForAll.Shepherd.Core
{
	public interface IRegistrationConstraintBehavior
	{
		bool ShouldSkipAutoRegistration(IServiceTypeMap typeMap);
	}
}