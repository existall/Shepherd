using System;
using System.Linq;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	internal static class TestUtils
	{
		public static IServiceTypeMap BuildServiceDescriptor(Type serviceType, Type implType, params Type[] types)
		{
			var allTypes = types.Concat(new[] { implType });

			return new RegistrationActions.ServiceTypeMap(serviceType, allTypes);
		}

		public static Type GetType<T>()
		{
			return typeof(T);
		}
	}
}