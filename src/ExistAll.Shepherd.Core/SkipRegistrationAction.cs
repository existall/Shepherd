using System;
using System.Linq;
using System.Reflection;
using ExistAll.Shepherd.Core.Resources;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
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

			return descriptor.ServiceType.GetCustomAttribute(AttributeType) != null || !descriptor.ImplementationTypes.Any();
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
		}
	}
}