using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions
{
	public interface IRegistrationBehavior
	{
		bool ShouldRegister(IServiceTypeMap typeMap);
		void Register(IRegistrationContext context, Container container);
	}
}