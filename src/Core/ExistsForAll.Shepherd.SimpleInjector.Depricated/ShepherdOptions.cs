using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public class ShepherdOptions : IShepherdOptions
	{
		public IServiceIndexer ServiceIndexer { get; set; } = new ServiceIndexer();
		public IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; } = new RegistrationConstraintBehavior();
		public IGenericRegistrationBehavior GenericRegistrationBehavior { get; set; } = new GenericRegistrationBehavior();
		public IDecoratorRegistrationBehavior DecoratorRegistrationBehavior { get; set; } = new DecoratorRegistrationBehavior();
		public ICollectionRegistrationBehavior CollectionRegistrationBehavior { get; set; } = new CollectionRegistrationBehavior();
		public ISingleServiceRegistrationBehavior SingleServiceRegistrationBehavior { get; set; } = new SingleServiceRegistrationBehavior();
	}
}