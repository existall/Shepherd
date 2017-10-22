using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface ISkipRegistration
	{
		bool ShouldSkipAutoRegistration(ICandidateDescriptor descriptor);
	}

	public class SkipRegistrationAction : ISkipRegistration
	{
		public Type AttributeType { get; set; } = typeof(SkipRegistrationAttribute);

		public virtual bool ShouldSkipAutoRegistration(ICandidateDescriptor descriptor)
		{
			if (AttributeType == null)
				throw new AutoRegistrationException(ExceptionMessages.SkipRegistrationMessage);

			if (descriptor.ServiceType.GetTypeInfo().GetCustomAttribute(AttributeType) != null)
				return true;

			if (descriptor.ServiceType.IsGeneric())
				return false;

			return !descriptor.ImplementationTypes.Any();
		}
	}
}