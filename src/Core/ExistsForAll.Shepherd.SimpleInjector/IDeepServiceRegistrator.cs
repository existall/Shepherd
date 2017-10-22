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
			var actions = new List<IRegistrationActionCandidate>
			{
				options.GenericRegistration,
				options.DecoratorRegistration,
				options.CollectionRegistration,
				options.SingleServiceRegistration
			};

			foreach (var index in typeIndex)
			{
				var candidateDescriptor = new ServiceDescriptor(index.ServiceType, index.ImplementationTypes);

				if (options.SkipRegistration.ShouldSkipAutoRegistration(candidateDescriptor))
				{
					continue;
				}

				IterateActions(actions,candidateDescriptor,container,assemblies);
			}
		}

		private static void IterateActions(List<IRegistrationActionCandidate> actions,
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