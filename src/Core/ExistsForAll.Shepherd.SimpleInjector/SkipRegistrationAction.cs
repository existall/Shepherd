using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface ISkipRegistration : IRegistrationActionCandidate
	{ }

	public class SkipRegistrationAction : ISkipRegistration
	{
		public Type AttributeType { get; set; } = typeof(SkipRegistrationAttribute);

		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			if (AttributeType == null)
				throw new AutoRegistrationException(ExceptionMessages.SkipRegistrationMessage);

			return descriptor.ServiceType.GetTypeInfo().GetCustomAttribute(AttributeType) != null || !descriptor.ImplementationTypes.Any();
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
		}
	}
}