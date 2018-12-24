using System.Collections.Generic;
using System.Reflection;
using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	internal class AutoRegistrationBehavior<TContainer> : IAutoRegistrationBehavior<TContainer>
	{
		public void Register(TContainer container,
			IShepherdOptions<TContainer> options,
			IEnumerable<ServiceTypeMap> typeIndex,
			Assembly[] assemblies)
		{
			var actions = new List<IRegistrationBehavior<TContainer>>
			{
				options.GenericRegistrationBehavior,
				options.DecoratorRegistrationBehavior,
				options.CollectionRegistrationBehavior,
				options.SingleServiceRegistrationBehavior
			};

			foreach (var index in typeIndex)
			{
				var candidateDescriptor = new ServiceTypeMap(index.ServiceType, index.ImplementationTypes);

				if (options.RegistrationConstraintBehavior.ShouldSkipAutoRegistration(candidateDescriptor))
				{
					continue;
				}

				IterateActions(actions, candidateDescriptor, container, assemblies, options);
			}
		}

		private static void IterateActions(List<IRegistrationBehavior<TContainer>> actions,
			IServiceTypeMap serviceTypeMap,
			TContainer container,
			Assembly[] assemblies,
			IShepherdOptions<TContainer> options)
		{

			var context = new RegistrationContext<TContainer>(serviceTypeMap.ServiceType,
				serviceTypeMap.ImplementationTypes,
				assemblies,
				container);

			foreach (var item in options.Items)
			{
				context.Properties.Add(item.Key, item.Value);
			}
			
			foreach (var action in actions)
			{
				if (action.ShouldRegister(serviceTypeMap))
				{
					action.Register(context);
					return;
				}
			}
		}
	}
}