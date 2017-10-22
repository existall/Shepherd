using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IShepherdOptions
	{
		IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		IServiceIndexer ServiceIndexer { get; set; }
		ISkipRegistration SkipRegistration { get; set; }
		IGenericRegistration GenericRegistration { get; set; }
		IDecoratorRegistration DecoratorRegistration { get; set; }
		ICollectionRegistration CollectionRegistration { get; set; }
		ISingleServiceRegistration SingleServiceRegistration { get; set; }
	}
}