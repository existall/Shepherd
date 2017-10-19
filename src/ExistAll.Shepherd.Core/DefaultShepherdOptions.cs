namespace ExistAll.Shepherd.Core
{
	public class ShepherdOptions : IShepherdOptions
	{
		public IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		public ITypeMatcher TypeMatcher { get; set; }
		public ISkipRegistration SkipRegistration { get; set; }
	}
}