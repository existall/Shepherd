using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IGenericRegistrationBehavior : IRegistrationBehavior
	{ }

	public class GenericRegistrationBehavior : IGenericRegistrationBehavior
	{
		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return false;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{

		}
	}
}