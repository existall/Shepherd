using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	internal static class InternalShepherdExtensions
	{
		public static IRegistrationContext AsRegistrationContext(this IServiceDescriptor serviceDescriptor, IEnumerable<Assembly> assemblies)
		{
			return new RegistrationContext(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationTypes, assemblies);
		}

		public static IServiceDescriptor AsRegistrationContext(this IRegistrationContext context)
		{
			return new ServiceDescriptor(context.ServiceType, context.ImplementationTypes);
		}
	}
}