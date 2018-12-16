using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IRegistrationBehavior
	{
		bool ShouldRegister(IServiceTypeMap typeMap);
		void Register(IRegistrationContext context, Container container);
	}
}