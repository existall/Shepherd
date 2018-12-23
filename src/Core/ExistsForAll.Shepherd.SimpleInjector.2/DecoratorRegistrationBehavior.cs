using System;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector._2
{
	public class DecoratorRegistrationBehavior : IDecoratorRegistrationBehavior<Container>
	{
		public virtual bool ShouldRegister(IServiceTypeMap typeMap)
		{
			var count = typeMap.ImplementationTypes.Count();

			if (count <= 1)
				return false;

			var shouldRegister =  typeMap.ImplementationTypes
				.Any(implementationType =>
					HasSameServiceAsDependency(implementationType, typeMap.ServiceType));

			if(shouldRegister && count != 2)
				throw new DecoratorRegistrationException(typeMap);

			return shouldRegister;
		}

		public void Register(IRegistrationContext<Container> context)
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

			RegisterServices(context.Container, context.ServiceType, types[serviceIndex], types[decoratorIndex]);
		}

		private static void RegisterServices(Container container, Type serviceType, Type implementationType, Type decoratorType)
		{
			container.Register(serviceType, implementationType);
			container.RegisterDecorator(serviceType, decoratorType);
		}

		private bool HasSameServiceAsDependency(Type implementor, Type serviceType)
		{
			return implementor.GetTypeInfo().GetConstructors()
				.Select(ctor => ctor.GetParameters()
					.Any(x => x.ParameterType == serviceType))
				.FirstOrDefault();
		}
	}
}