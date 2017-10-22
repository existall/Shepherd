using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IGenericRegistration : IRegistrationActionCandidate
	{ }

	public class GenericRegistration : IGenericRegistration
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