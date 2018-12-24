using System.Linq;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class SingleServiceRegistrationBehavior : ISingleServiceRegistrationBehavior<IServiceCollection>
	{
		public bool ShouldRegister(IServiceTypeMap typeMap)
		{
			return typeMap.ImplementationTypes.Count() == 1;
		}

		public void Register(IRegistrationContext<IServiceCollection> context)
		{
			var serviceDescriptor = new ServiceDescriptor(context.ServiceType, context.ImplementationTypes.First(),
				context.GetDefaultLifeStyle());

			context.Container.TryAdd(serviceDescriptor);
		}
	}
}
