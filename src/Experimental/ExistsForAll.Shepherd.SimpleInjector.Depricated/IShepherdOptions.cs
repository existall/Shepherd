using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public interface IShepherdOptions
	{
		IServiceIndexer ServiceIndexer { get; set; }
		IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; }
		IGenericRegistrationBehavior GenericRegistrationBehavior { get; set; }
		IDecoratorRegistrationBehavior DecoratorRegistrationBehavior { get; set; }
		ICollectionRegistrationBehavior CollectionRegistrationBehavior { get; set; }
		ISingleServiceRegistrationBehavior SingleServiceRegistrationBehavior { get; set; }
	}
}