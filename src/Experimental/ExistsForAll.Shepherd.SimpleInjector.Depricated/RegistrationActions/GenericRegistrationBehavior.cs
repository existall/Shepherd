using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions
{
	public class GenericRegistrationBehavior : IGenericRegistrationBehavior
	{
		public virtual bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return false;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{

		}
	}
}