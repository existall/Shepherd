namespace ExistsForAll.Shepherd.SimpleInjector
{
	internal interface IOptionsValidator
	{
		void ValidateOptions(IShepherdOptions options);
	}

	internal class OptionsValidator : IOptionsValidator
	{
		public void ValidateOptions(IShepherdOptions options)
		{
			Guard.NullArgument(options.DecoratorRegistration, nameof(options.DecoratorRegistration));
			Guard.NullArgument(options.CollectionRegistration, nameof(options.CollectionRegistration));
			Guard.NullArgument(options.ConfigureContainerOptions, nameof(options.ConfigureContainerOptions));
			Guard.NullArgument(options.GenericRegistration, nameof(options.GenericRegistration));
			Guard.NullArgument(options.SingleServiceRegistration, nameof(options.SingleServiceRegistration));
			Guard.NullArgument(options.SkipRegistration, nameof(options.SkipRegistration));
			Guard.NullArgument(options.ServiceIndexer, nameof(options.ServiceIndexer));
		}
	}
}