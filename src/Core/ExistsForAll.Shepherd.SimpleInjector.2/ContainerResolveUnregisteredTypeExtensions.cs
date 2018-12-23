using System;
using System.Collections.Generic;
using System.Linq;
using ExistsForAll.Shepherd.Core.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector._2
{
	internal static class ContainerResolveUnregisteredTypeExtensions
	{
		public static Container AddSingleAsCollectionSupport(this Container container)
		{
			container.ResolveUnregisteredType += (s, e) =>
			{
				if (e.Handled)
					return;

				var serviceType = e.UnregisteredServiceType;

				if (serviceType.IsArray)
				{
					var elementType = serviceType.GetElementType();
					var producer = container.GetRegistration(elementType);
					
					if(producer == null)
						return;

					var array = Array.CreateInstance(elementType, 1);
					
					array.SetValue(producer.GetInstance(), 0);
					
					e.Register(producer.Lifestyle.CreateRegistration(serviceType, () => array, container));

					return;
				}

				if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					var elementType = serviceType.GetGenericArguments().Single();
					var producer = container.GetRegistration(elementType);

					if (producer == null)
						return;

					var castMethod = typeof(Enumerable)
						.Info()
						.GetMethod("Cast")
						.MakeGenericMethod(elementType);

					object stream = new[] {producer.GetInstance()}.Select(x => x);

					stream = castMethod.Invoke(null, new[] {stream});

					e.Register(producer.Lifestyle.CreateRegistration(serviceType, () => stream, container));
				}
			};

			return container;
		}
	}
}