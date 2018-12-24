using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal class AutoRegistrationBehavior : IAutoRegistrationBehavior
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
				var candidateDescriptor = new RegistrationActions.ServiceTypeMap(index.ServiceType, index.ImplementationTypes);

				if (options.RegistrationConstraintBehavior.ShouldSkipAutoRegistration(candidateDescriptor))
				{
					continue;
				}

				IterateActions(actions,candidateDescriptor,container,assemblies);
			}
		}

		private static void IterateActions(List<IRegistrationBehavior> actions,
			IServiceTypeMap serviceTypeMap,
			Container container,
			Assembly[] assemblies)
		{
			foreach (var action in actions)
			{
				if (action.ShouldRegister(serviceTypeMap))
				{
					action.Register(new RegistrationContext(serviceTypeMap.ServiceType,
							serviceTypeMap.ImplementationTypes,
							assemblies),
						container);
					return;
				}
			}
		}
	}
}