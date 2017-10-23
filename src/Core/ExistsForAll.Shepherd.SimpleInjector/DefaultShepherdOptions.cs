using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class ShepherdOptions : IShepherdOptions
	{
		public IContainerOptionsConfiguration ConfigureContainerOptions { get; set; } = new DefaultContainerOptionsConfiguration();
		public IServiceIndexer ServiceIndexer { get; set; } = new ServiceIndexer();
		public IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; } = new RegistrationConstraintBehavior();
		public IGenericRegistrationBehavior GenericRegistrationBehavior { get; set; } = new GenericRegistrationBehavior();
		public IDecoratorRegistrationBehavior DecoratorRegistrationBehavior { get; set; } = new DecoratorRegistrationBehavior();
		public ICollectionRegistrationBehavior CollectionRegistrationBehavior { get; set; } = new CollectionRegistrationBehavior();
		public ISingleServiceRegistrationBehavior SingleServiceRegistrationBehavior { get; set; } = new SingleServiceRegistrationBehavior();
	}
}