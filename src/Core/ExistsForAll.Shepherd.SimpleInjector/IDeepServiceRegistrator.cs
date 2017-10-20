using System;
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
				var candidateDescriptor = new CandidateDescriptor(index.ServiceType, index.ImplementationTypes);

				if (options.SkipRegistration.ShouldSkipAutoRegistration(candidateDescriptor))
				{
					continue;
				}

				IterateActions(actions,candidateDescriptor,container,assemblies);
			}
		}

		private void IterateActions(List<IRegistrationActionCandidate> actions,
			ICandidateDescriptor candidateDescriptor,
			Container container, Assembly[] assemblies)
		{
			foreach (var action in actions)
			{
				if (action.ShouldRegister(candidateDescriptor))
				{
					action.Register(new RegistrationContext(candidateDescriptor.ServiceType,
						candidateDescriptor.ImplementationTypes,
						assemblies),
						container);
					return;
				}
			}
		}
	}
}