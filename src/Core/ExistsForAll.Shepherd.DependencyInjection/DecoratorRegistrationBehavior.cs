using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class DecoratorRegistrationBehavior : IDecoratorRegistrationBehavior<IServiceCollection>
	{
		public bool ShouldRegister(IServiceTypeMap map)
		{
			var count = map.ImplementationTypes.Count();

			if (count <= 1)
				return false;

			var shouldRegister =  map.ImplementationTypes
				.Any(implementationType =>
					HasSameServiceAsDependency(implementationType, map.ServiceType));

			if(shouldRegister && count != 2)
				throw new DecoratorRegistrationException(map);

			return shouldRegister;
		}


		public virtual void Register(IRegistrationContext<IServiceCollection> context)
		{
			var types = context.ImplementationTypes.ToArray();

			int decoratorIndex;
			int serviceIndex;

			if (HasSameServiceAsDependency(types[0], context.ServiceType))
			{
				decoratorIndex = 0;
				serviceIndex = 1;
			}
			else
			{
				decoratorIndex = 1;
				serviceIndex = 0;
			}

		    RegisterServices(context.Container,
		        context.ServiceType,
		        types[serviceIndex],
		        types[decoratorIndex],
		        context.GetDefaultLifeStyle());
		}

		private static void RegisterServices(IServiceCollection container,
		    Type serviceType,
		    Type implementationType,
		    Type decoratorType,
		    ServiceLifetime lifetime)
		{
			container.TryAdd(new ServiceDescriptor(serviceType, implementationType, lifetime));
			container.TryDecorate(serviceType, decoratorType);
		}

		private static bool HasSameServiceAsDependency(Type implementor, Type serviceType)
		{
			var e =  implementor.GetTypeInfo()
			    .GetConstructors()
				.Select(ctor => ctor.GetParameters().Any(x => x.ParameterType == serviceType))
				.FirstOrDefault();

		    return e;
		}
	}
}
