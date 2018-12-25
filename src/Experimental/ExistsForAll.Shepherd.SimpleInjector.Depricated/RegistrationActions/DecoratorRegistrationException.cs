using System;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions
{
	public class DecoratorRegistrationException : Exception
	{
		public DecoratorRegistrationException(IServiceTypeMap serviceTypeMap)
			: base(GetMessage(serviceTypeMap))
		{
		}

		public DecoratorRegistrationException(IServiceTypeMap serviceTypeMap, Exception inner)
			: base(GetMessage(serviceTypeMap), inner)
		{
		}

		private static string GetMessage(IServiceTypeMap serviceTypeMap) =>
			ExceptionMessages.DecoratorRegistrationExceptionMessage(serviceTypeMap.ServiceType,
				serviceTypeMap.ImplementationTypes);

	}
}