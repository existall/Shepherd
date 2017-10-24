using System;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class ShepherdTests
	{
		[Fact]
		public void Herd_FullIntegrationTest()
		{
			var sut = new Shepherd();
			sut.AddCompleteTypeAssembly(typeof(INoImplInterface).Assembly);
			var container = sut.Herd();

			container.Verify();

			AssertServiceRegistration<DecoratorServiceDecorator>(typeof(IDecoratorService), i =>
			{
				Assert.IsType<DecoratorServiceDecorator>(i);
				Assert.IsType<DecoratorService>(i.DecoratorService);
			});

			AssertServiceRegistration<SingleImplService>(typeof(ISingleImplService), i =>
			{
				Assert.IsType<SingleImplService>(i);
			});

			void AssertServiceRegistration<T>(Type serviceType, Action<T> assertionAction)
			{
				var instance = (T)container.GetInstance(serviceType);

				assertionAction.Invoke(instance);
			}
		}

		
	}
}
