using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions
{
	internal static class InternalShepherdExtensions
	{
		public static IRegistrationContext AsRegistrationContext(this IServiceTypeMap serviceTypeMap, IEnumerable<Assembly> assemblies)
		{
			return new RegistrationContext(serviceTypeMap.ServiceType, serviceTypeMap.ImplementationTypes, assemblies);
		}

		public static IServiceTypeMap AsServiceDescriptor(this IRegistrationContext context)
		{
			return new RegistrationActions.ServiceTypeMap(context.ServiceType, context.ImplementationTypes);
		}
	}
}