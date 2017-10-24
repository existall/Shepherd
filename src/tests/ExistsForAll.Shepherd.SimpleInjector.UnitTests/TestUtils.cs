using System;
using System.Linq;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	internal static class TestUtils
	{
		public static IServiceDescriptor BuildServiceDescriptor(Type serviceType, Type implType, params Type[] types)
		{
			var allTypes = types.Concat(new[] { implType });

			return new ServiceDescriptor(serviceType, allTypes);
		}

		public static Type GetType<T>()
		{
			return typeof(T);
		}
	}
}