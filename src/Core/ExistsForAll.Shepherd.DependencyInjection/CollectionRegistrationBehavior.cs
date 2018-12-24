using System.Linq;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class CollectionRegistrationBehavior : ICollectionRegistrationBehavior<IServiceCollection>
	{
		public bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return typeMap.ImplementationTypes.Count() > 1;
		}

		public virtual void Register(IRegistrationContext<IServiceCollection> context)
		{
		     var descriptors = context.ImplementationTypes.Select(x =>
		            new ServiceDescriptor(context.ServiceType, x,
		                context.GetDefaultLifeStyle()))
		         .ToArray();

		     context.Container.Add(descriptors);
		}
	}
}
