namespace ExistsForAll.Shepherd.SimpleInjector.Resources
{
	internal static class ExceptionMessages
	{
		public const string SkipRegistrationMessage = @"SkipRegistration Attribute was not set for SkipRegistration in ShepheredOptions.
An attribute type must be set in order for the current configuration.
Another functionality can be provided by overloading Register method or replacing ISkipRegistration interface";

		public const string MissingTypeFilterMessage = @"While gathering all service types and implemintations in TypeMatcher.MapTypes method
 the TypeFilter predicat is null. Please provide one";
	}
}