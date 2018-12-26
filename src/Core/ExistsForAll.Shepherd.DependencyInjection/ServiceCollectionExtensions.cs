using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public static partial class ServiceCollectionExtensions
    {
	    public static IServiceCollection Scan(this IServiceCollection target,
		    Action<ShepherdBuilder<IServiceCollection>> action)
	    {
		    if (action == null) throw new ArgumentNullException(nameof(action));

		    var shepherd = new ServiceCollectionShepherd(target);

		    var builder = new ShepherdBuilder<IServiceCollection>(shepherd);

		    action(builder);

		    shepherd.Herd();

		    return target;
	    }

	    public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
            where TDecorator : TService
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        return services.DecorateDescriptors(typeof(TService), x => x.Decorate(typeof(TDecorator)));
        }

        public static bool TryDecorate<TService, TDecorator>(this IServiceCollection services)
            where TDecorator : TService
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));

            return services.TryDecorateDescriptors(typeof(TService), x => x.Decorate(typeof(TDecorator)));
        }

        public static IServiceCollection Decorate(this IServiceCollection services,
	        Type serviceType,
            Type decoratorType)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decoratorType == null) throw new ArgumentNullException(nameof(decoratorType));

	        return services.DecorateDescriptors(serviceType, x => x.Decorate(decoratorType));
        }

        public static bool TryDecorate(this IServiceCollection services, Type serviceType, Type decoratorType)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decoratorType == null) throw new ArgumentNullException(nameof(decoratorType));
	        
	        return services.TryDecorateDescriptors(serviceType, x => x.Decorate(decoratorType));
        }

        public static IServiceCollection Decorate<TService>(this IServiceCollection services,
            Func<TService, IServiceProvider, TService> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.DecorateDescriptors(typeof(TService), x => x.Decorate(decorator));
        }

        public static bool TryDecorate<TService>(this IServiceCollection services,
            Func<TService, IServiceProvider, TService> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));

	        return services.TryDecorateDescriptors(typeof(TService), x => x.Decorate(decorator));
        }

        public static IServiceCollection Decorate<TService>(this IServiceCollection services,
            Func<TService, TService> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.DecorateDescriptors(typeof(TService), x => x.Decorate(decorator));
        }

        public static bool TryDecorate<TService>(this IServiceCollection services, Func<TService, TService> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.TryDecorateDescriptors(typeof(TService), x => x.Decorate(decorator));
        }

        public static IServiceCollection Decorate(this IServiceCollection services,
	        Type serviceType,
            Func<object, IServiceProvider, object> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.DecorateDescriptors(serviceType, x => x.Decorate(decorator));
        }

        public static bool TryDecorate(this IServiceCollection services, Type serviceType,
            Func<object, IServiceProvider, object> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.TryDecorateDescriptors(serviceType, x => x.Decorate(decorator));
        }

        public static IServiceCollection Decorate(this IServiceCollection services, Type serviceType,
            Func<object, object> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.DecorateDescriptors(serviceType, x => x.Decorate(decorator));
        }

        public static bool TryDecorate(this IServiceCollection services, Type serviceType,
            Func<object, object> decorator)
        {
	        if (services == null) throw new ArgumentNullException(nameof(services));
	        if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
	        if (decorator == null) throw new ArgumentNullException(nameof(decorator));
	        return services.TryDecorateDescriptors(serviceType, x => x.Decorate(decorator));
        }

        private static IServiceCollection DecorateOpenGeneric(this IServiceCollection services, Type serviceType,
            Type decoratorType)
        {
            if (services.TryDecorateOpenGeneric(serviceType, decoratorType))
            {
                return services;
            }

            throw new Exception("serviceType");
        }

        private static bool TryDecorateOpenGeneric(this IServiceCollection services, Type serviceType,
            Type decoratorType)
        {
            bool TryDecorate(Type[] typeArguments)
            {
                var closedServiceType = serviceType.MakeGenericType(typeArguments);
                var closedDecoratorType = decoratorType.MakeGenericType(typeArguments);

                return services.TryDecorateDescriptors(closedServiceType, x => x.Decorate(closedDecoratorType));
            }

            var arguments = services
                .Where(descriptor =>
                    descriptor.ServiceType
	                    .GetTypeInfo()
	                    .IsAssignableTo(serviceType))
                .Select(descriptor => descriptor.ServiceType.GenericTypeArguments)
                .ToArray();

            if (arguments.Length == 0)
            {
                return false;
            }

            return arguments.Aggregate(true, (result, args) => result && TryDecorate(args));
        }

        private static IServiceCollection DecorateDescriptors(this IServiceCollection services, Type serviceType,
            Func<ServiceDescriptor, ServiceDescriptor> decorator)
        {
            if (services.TryDecorateDescriptors(serviceType, decorator))
            {
                return services;
            }

            throw new Exception("serviceType");
        }

        private static bool TryDecorateDescriptors(this IServiceCollection services, Type serviceType,
            Func<ServiceDescriptor, ServiceDescriptor> decorator)
        {
            if (!services.TryGetDescriptors(serviceType, out var descriptors))
            {
                return false;
            }

            foreach (var descriptor in descriptors)
            {
                var index = services.IndexOf(descriptor);

                // To avoid reordering descriptors, in case a specific order is expected.
                services.Insert(index, decorator(descriptor));

                services.Remove(descriptor);
            }

            return true;
        }

        private static bool TryGetDescriptors(this IServiceCollection services, Type serviceType,
            out ICollection<ServiceDescriptor> descriptors)
        {
            return (descriptors = services.Where(service => service.ServiceType == serviceType).ToArray()).Any();
        }

        private static ServiceDescriptor Decorate<TService>(this ServiceDescriptor descriptor,
            Func<TService, IServiceProvider, TService> decorator)
        {
            return descriptor.WithFactory(provider => decorator((TService) provider.GetInstance(descriptor), provider));
        }

        private static ServiceDescriptor Decorate<TService>(this ServiceDescriptor descriptor,
            Func<TService, TService> decorator)
        {
            return descriptor.WithFactory(provider => decorator((TService) provider.GetInstance(descriptor)));
        }

        private static ServiceDescriptor Decorate(this ServiceDescriptor descriptor, Type decoratorType)
        {
            return descriptor.WithFactory(provider =>
                provider.CreateInstance(decoratorType, provider.GetInstance(descriptor)));
        }

        private static ServiceDescriptor WithFactory(this ServiceDescriptor descriptor,
            Func<IServiceProvider, object> factory)
        {
            return ServiceDescriptor.Describe(descriptor.ServiceType, factory, descriptor.Lifetime);
        }

        private static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
            }

            return descriptor.ImplementationFactory(provider);
        }

        private static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
        }

        private static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(provider, type, arguments);
        }
    }
}

