﻿using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class ShepherdOptions : IShepherdOptions
	{
		public IContainerOptionsConfiguration ConfigureContainerOptions { get; set; } = new DefaultContainerOptionsConfiguration();
		public IServiceIndexer ServiceIndexer { get; set; } = new ServiceIndexer();
		public ISkipRegistration SkipRegistration { get; set; } = new SkipRegistrationAction();
		public IGenericRegistration GenericRegistration { get; set; } = new GenericRegistration();
		public IDecoratorRegistration DecoratorRegistration { get; set; } = new DecoratorRegistration();
		public ICollectionRegistration CollectionRegistration { get; set; } = new CollectionRegistration();
		public ISingleServiceRegistration SingleServiceRegistration { get; set; } = new SingleServiceRegistration();
	}
}