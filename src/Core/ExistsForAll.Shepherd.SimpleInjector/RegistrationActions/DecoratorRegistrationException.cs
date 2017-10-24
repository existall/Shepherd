using System;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public class DecoratorRegistrationException : Exception
	{
		public DecoratorRegistrationException(IServiceDescriptor serviceDescriptor)
			: base(GetMessage(serviceDescriptor))
		{
		}

		public DecoratorRegistrationException(IServiceDescriptor serviceDescriptor, Exception inner)
			: base(GetMessage(serviceDescriptor), inner)
		{
		}

		private static string GetMessage(IServiceDescriptor serviceDescriptor) =>
			ExceptionMessages.DecoratorRegistrationExceptionMessage(serviceDescriptor.ServiceType,
				serviceDescriptor.ImplementationTypes);

	}
}