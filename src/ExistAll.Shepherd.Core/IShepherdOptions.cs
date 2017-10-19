namespace ExistAll.Shepherd.Core
{
	public interface IShepherdOptions
	{
		IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		ITypeMatcher TypeMatcher { get; set; }
		ISkipRegistration SkipRegistration { get; set; }
	}
}