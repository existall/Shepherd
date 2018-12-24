using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class GenericRegistrationBehavior : IGenericRegistrationBehavior<Container>
	{
		public virtual bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return false;
		}

		public void Register(IRegistrationContext<Container> context)
		{
			
		}
	}
}