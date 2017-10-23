﻿using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IShepherdOptions
	{
		IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		IServiceIndexer ServiceIndexer { get; set; }
		IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; }
		IGenericRegistrationBehavior GenericRegistrationBehavior { get; set; }
		IDecoratorRegistrationBehavior DecoratorRegistrationBehavior { get; set; }
		ICollectionRegistrationBehavior CollectionRegistrationBehavior { get; set; }
		ISingleServiceRegistrationBehavior SingleServiceRegistrationBehavior { get; set; }
	}
}