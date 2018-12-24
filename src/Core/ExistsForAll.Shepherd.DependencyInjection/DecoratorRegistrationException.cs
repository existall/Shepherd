using System;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Resources;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class DecoratorRegistrationException : Exception
	{
		public DecoratorRegistrationException(IServiceTypeMap serviceMap)
			: base(GetMessage(serviceMap))
		{
		}

		public DecoratorRegistrationException(IServiceTypeMap serviceMap, Exception inner)
			: base(GetMessage(serviceMap), inner)
		{
		}

		private static string GetMessage(IServiceTypeMap serviceMap) =>
			ExceptionMessages.DecoratorRegistrationExceptionMessage(serviceMap.ServiceType,
				serviceMap.ImplementationTypes);
	}
}
