namespace ExistAll.Shepherd.Core
{
	public interface IShepherdOptions
	{
		IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		ITypeMatcher TypeMatcher { get; set; }
		ISkipRegistration SkipRegistration { get; set; }
		IGenericRegistration GenericRegistration { get; set; }
		IDecoratorRegistration DecoratorRegistration { get; set; }
		ICollectionRegistration CollectionRegistration { get; set; }
		ISingleServiceRegistration SingleServiceRegistration { get; set; }
	}
}