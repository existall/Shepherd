using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
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
					var elementType1 = serviceType.GetElementType();
					var producer1 = container.GetRegistration(elementType1);
					
					if(producer1 == null) 
						return;

					var array = Array.CreateInstance(elementType1, 1);
					
					array.SetValue(producer1.GetInstance(),0);

					var castMethod1 = typeof(Enumerable)
						.Info()
						.GetMethod("Cast")
						.MakeGenericMethod(elementType1);

					object stream1 = new[] {producer1.GetInstance()}.Select(x => x);
					
					stream1 = castMethod1.Invoke(null, new[] {array});
					
					e.Register(producer1.Lifestyle.CreateRegistration(serviceType, () => stream1, container));
				}
				
				if (!serviceType.IsGenericType() || serviceType.GetGenericTypeDefinition() != typeof(IEnumerable<>)) 
					return;
				
				var elementType = serviceType.GetGenericArguments().Single();
				var producer = container.GetRegistration(elementType);
				
				if(producer == null) 
					return;
					
				// Create a stream --> array should be handled differntly !!!!

				var castMethod = typeof(Enumerable)
					.Info()
					.GetMethod("Cast")
					.MakeGenericMethod(elementType);
					
				object stream = new[] {producer.GetInstance()}.Select(x => x);
					
				stream = castMethod.Invoke(null, new[] {stream});

				e.Register(producer.Lifestyle.CreateRegistration(serviceType, () => stream, container));
			};

			return container;
		}
	}
}