namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects
{
	public class DecoratorServiceDecorator : IDecoratorService
	{
		public IDecoratorService DecoratorService { get; }

		public DecoratorServiceDecorator(IDecoratorService decoratorService)
		{
			DecoratorService = decoratorService;
		}
	}
}