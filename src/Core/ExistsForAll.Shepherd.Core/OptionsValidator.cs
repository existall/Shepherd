namespace ExistsForAll.Shepherd.Core
{
	internal class OptionsValidator<TContainer> : IOptionsValidator<TContainer>
	{
		public void ValidateOptions(IShepherdOptions<TContainer> options)
		{
			Guard.NullArgument(options.DecoratorRegistrationBehavior, nameof(options.DecoratorRegistrationBehavior));
			Guard.NullArgument(options.CollectionRegistrationBehavior, nameof(options.CollectionRegistrationBehavior));
			Guard.NullArgument(options.GenericRegistrationBehavior, nameof(options.GenericRegistrationBehavior));
			Guard.NullArgument(options.SingleServiceRegistrationBehavior, nameof(options.SingleServiceRegistrationBehavior));
			Guard.NullArgument(options.RegistrationConstraintBehavior, nameof(options.RegistrationConstraintBehavior));
			Guard.NullArgument(options.ServiceIndexer, nameof(options.ServiceIndexer));
		}
	}
}