using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class ServiceCollectionOptions : ShepherdOptions<IServiceCollection>
	{
		public override IGenericRegistrationBehavior<IServiceCollection> GenericRegistrationBehavior { get; set; } = new GenericRegistrationBehavior();
		public override IDecoratorRegistrationBehavior<IServiceCollection> DecoratorRegistrationBehavior { get; set; } = new DecoratorRegistrationBehavior();
		public override ICollectionRegistrationBehavior<IServiceCollection> CollectionRegistrationBehavior { get; set; } = new CollectionRegistrationBehavior();
		public override ISingleServiceRegistrationBehavior<IServiceCollection> SingleServiceRegistrationBehavior { get; set; } = new  SingleServiceRegistrationBehavior();
	}
}