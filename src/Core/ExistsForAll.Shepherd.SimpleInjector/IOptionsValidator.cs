﻿namespace ExistsForAll.Shepherd.SimpleInjector
{
	internal interface IOptionsValidator
	{
		void ValidateOptions(IShepherdOptions options);
	}

	internal class OptionsValidator : IOptionsValidator
	{
		public void ValidateOptions(IShepherdOptions options)
		{
			Guard.NullArgument(options.DecoratorRegistrationBehavior, nameof(options.DecoratorRegistrationBehavior));
			Guard.NullArgument(options.CollectionRegistrationBehavior, nameof(options.CollectionRegistrationBehavior));
			Guard.NullArgument(options.ConfigureContainerOptions, nameof(options.ConfigureContainerOptions));
			Guard.NullArgument(options.GenericRegistrationBehavior, nameof(options.GenericRegistrationBehavior));
			Guard.NullArgument(options.SingleServiceRegistrationBehavior, nameof(options.SingleServiceRegistrationBehavior));
			Guard.NullArgument(options.RegistrationConstraintBehavior, nameof(options.RegistrationConstraintBehavior));
			Guard.NullArgument(options.ServiceIndexer, nameof(options.ServiceIndexer));
		}
	}
}