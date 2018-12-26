using System;
using System.ComponentModel.Design;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Extensions;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class GenericRegistrationBehavior : IGenericRegistrationBehavior<IServiceCollection>
	{
		public virtual bool ShouldRegister(IServiceTypeMap map)
		{
		    return map.ServiceType.IsGeneric();
		}

		public virtual void Register(IRegistrationContext<IServiceCollection> context)
		{
		    foreach (var type in context.ImplementationTypes)
		    {
		        if (type.IsGeneric())
		        {
		            RegisterServiceDescriptor(context.Container, context.ServiceType, type, context.GetDefaultLifeStyle());
		            continue;
		        }

		        foreach (var @interface in type.GetInterfaces())
		        {
		            if (@interface.IsAssignableTo(context.ServiceType))
		            {
		                RegisterServiceDescriptor(context.Container, @interface, type, context.GetDefaultLifeStyle());
		            }
		        }
		    }
		}

	    private static void RegisterServiceDescriptor(IServiceCollection services,
	        Type serviceType,
	        Type implementationsType,
	        ServiceLifetime lifetime)
	    {
	        var descriptor = ServiceDescriptor.Describe(serviceType, implementationsType, lifetime);
	        services.TryAdd(descriptor);
	    }
	}
}
