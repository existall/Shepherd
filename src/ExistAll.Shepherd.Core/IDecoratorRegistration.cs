using System;
using System.Linq;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IDecoratorRegistration : IRegistrationActionCandidate
	{

	}

	public class DecoratorRegistration : IDecoratorRegistration
	{
		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			var count = descriptor.ImplementationTypes.Count();

			if (count != 2)
				return false;

			return descriptor.ImplementationTypes
				.Any(implementationType =>
				HasSameServiceAsDependency(implementationType, descriptor.ServiceType));

		}

		public virtual void Register(IRegistrationContext context, Container container)
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

			RegisterServices(container, context.ServiceType, types[serviceIndex], types[decoratorIndex]);
		}

		private void RegisterServices(Container container, Type serviceType, Type implementationType, Type decoratorType)
		{
			container.Register(serviceType, implementationType);
			container.RegisterDecorator(serviceType, decoratorType);
		}

		private bool HasSameServiceAsDependency(Type implementor, Type serviceType)
		{
			return implementor.GetConstructors()
				.Select(ctor => ctor.GetParameters()
				.Any(x => x.ParameterType == serviceType))
				.FirstOrDefault();
		}
	}
}