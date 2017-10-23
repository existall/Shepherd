using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	internal interface IDeepServiceRegistrator
	{
		void Register(Container container,
			IShepherdOptions options,
			IEnumerable<ServiceTypeMap> typeIndex,
			Assembly[] assemblies);
	}

	internal class DeepServiceRegistrator : IDeepServiceRegistrator
	{
		public void Register(Container container,
			IShepherdOptions options,
			IEnumerable<ServiceTypeMap> typeIndex,
			Assembly[] assemblies)
		{
			var actions = new List<IRegistrationBehavior>
			{
				options.GenericRegistrationBehavior,
				options.DecoratorRegistrationBehavior,
				options.CollectionRegistrationBehavior,
				options.SingleServiceRegistrationBehavior
			};

			foreach (var index in typeIndex)
			{
				var candidateDescriptor = new ServiceDescriptor(index.ServiceType, index.ImplementationTypes);

				if (options.RegistrationConstraintBehavior.ShouldSkipAutoRegistration(candidateDescriptor))
				{
					continue;
				}

				IterateActions(actions,candidateDescriptor,container,assemblies);
			}
		}

		private static void IterateActions(List<IRegistrationBehavior> actions,
			IServiceDescriptor serviceDescriptor,
			Container container,
			Assembly[] assemblies)
		{
			foreach (var action in actions)
			{
				if (action.ShouldRegister(serviceDescriptor))
				{
					action.Register(new RegistrationContext(serviceDescriptor.ServiceType,
						serviceDescriptor.ImplementationTypes,
						assemblies),
						container);
					return;
				}
			}
		}
	}
}