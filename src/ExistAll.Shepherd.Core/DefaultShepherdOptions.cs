namespace ExistAll.Shepherd.Core
{
	public class ShepherdOptions : IShepherdOptions
	{
		public IContainerOptionsConfiguration ConfigureContainerOptions { get; set; } = new DefaultContainerOptionsConfiguration();
		public ITypeMatcher TypeMatcher { get; set; } = new TypeMatcher();
		public ISkipRegistration SkipRegistration { get; set; } = new SkipRegistrationAction();
		public IGenericRegistration GenericRegistration { get; set; }
		public IDecoratorRegistration DecoratorRegistration { get; set; } = new DecoratorRegistration();
		public ICollectionRegistration CollectionRegistration { get; set; } = new CollectionRegistration();
		public ISingleServiceRegistration SingleServiceRegistration { get; set; } = new SingleServiceRegistration();

		public ShepherdOptions()
		{
			GenericRegistration = new GenericRegistration(this);
		}
	}
}