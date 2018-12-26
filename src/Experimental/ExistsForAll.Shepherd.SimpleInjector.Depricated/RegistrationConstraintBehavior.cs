using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public class RegistrationConstraintBehavior : IRegistrationConstraintBehavior
	{
		public Type AttributeType { get; set; } = typeof(SkipRegistrationAttribute);

		public virtual bool ShouldSkipAutoRegistration(IServiceTypeMap typeMap)
		{
			if (AttributeType == null)
				throw new AutoRegistrationException(ExceptionMessages.SkipRegistrationMessage);

			if (typeMap.ServiceType.GetTypeInfo().GetCustomAttribute(AttributeType) != null)
				return true;

			return !typeMap.ImplementationTypes.Any();
		}
	}
}