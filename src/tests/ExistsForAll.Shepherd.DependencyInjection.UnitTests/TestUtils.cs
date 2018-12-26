using System;
using System.Linq;
using ExistsForAll.Shepherd.Core;

namespace ExistsForAll.Shepherd.DependencyInjection.UnitTests
{
    internal static class TestUtils
    {
        public static IServiceTypeMap BuildServiceMap(Type serviceType, Type implType, params Type[] types)
        {
            var allTypes = types.Concat(new[] { implType });

            return new ServiceTypeMap(serviceType, allTypes);
        }

        public static Type GetType<T>()
        {
            return typeof(T);
        }
    }
}