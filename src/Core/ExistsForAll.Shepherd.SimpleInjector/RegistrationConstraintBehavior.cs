using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class RegistrationConstraintBehavior : IRegistrationConstraintBehavior
	{
		public Type AttributeType { get; set; } = typeof(SkipRegistrationAttribute);

		public virtual bool ShouldSkipAutoRegistration(IServiceDescriptor descriptor)
		{
			if (AttributeType == null)
				throw new AutoRegistrationException(ExceptionMessages.SkipRegistrationMessage);

			if (descriptor.ServiceType.GetTypeInfo().GetCustomAttribute(AttributeType) != null)
				return true;

			return !descriptor.ImplementationTypes.Any();
		}
	}
}