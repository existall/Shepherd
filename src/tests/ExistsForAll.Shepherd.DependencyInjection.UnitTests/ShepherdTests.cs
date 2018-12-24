using System;
using System.Collections.Generic;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Filters;
using ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ExistsForAll.Shepherd.DependencyInjection.UnitTests
{
    public class ShepherdTests
	{
		[Fact]
		public void Herd_FullIntegrationTest()
		{
			var sut = new ServiceCollection()
			    .Scan(x => x.WithAssembly<INoImplInterface>()
			        .WithOptions(o => o.ServiceIndexer.Filters.Add(new InterfaceAccumulationFilter(typeof(IFilterService)))));

		    var provider = sut.BuildServiceProvider();

		    try
			{

				AssertServiceRegistration<DecoratorServiceDecorator>(typeof(IDecoratorService), i =>
				{
					Assert.IsType<DecoratorServiceDecorator>(i);
					Assert.IsType<DecoratorService>(i.DecoratorService);
				});

				AssertServiceRegistration<SingleImplService>(typeof(ISingleImplService),
					i => { Assert.IsType<SingleImplService>(i); });

				AssertServiceRegistration<OpenCloseGeneric>(typeof(IOpenCloseGeneric<int>), i =>
				{
					Assert.IsType<OpenCloseGeneric>(i);
					Assert.IsType<int>(i.TypeOfGeneric);
				});

				var openGenericInstance = provider.GetService(typeof(IOpenGenerics<int>));

				Assert.IsType<OpenGenerics<int>>(openGenericInstance);

				var collectionResults = provider.GetService<IEnumerable<ICollectionService>>();
				Assert.Collection(collectionResults, i => Assert.IsType<CollectionService1>(i),
					i => Assert.IsType<CollectionService2>(i),
					i => Assert.IsType<CollectionService3>(i));

				NotRegisteredAssertion(typeof(IFilterService));
				NotRegisteredAssertion(typeof(INoImplInterface));
			}
			finally
			{
				provider.Dispose();
			}

			void AssertServiceRegistration<T>(Type serviceType, Action<T> assertionAction)
			{
				var instance = (T) provider.GetService(serviceType);

				assertionAction.Invoke(instance);
			}

			void NotRegisteredAssertion(Type type)
			{
				Assert.Throws<InvalidOperationException>(() => provider.GetRequiredService(type));
			}
		}

		[Fact]
		public void Herd_ShouldSkipAutoRegistrationMark()
		{
			var services = new ServiceCollection()
			    .Scan(x => x.WithOptions(o => o.RegistrationConstraintBehavior =
			        new RegistrationConstraintBehavior()
			            {AttributeType = typeof(SkipRegistrationTestAttribute)})
			        .WithAssembly<INoImplInterface>());

		    var sut = services.BuildServiceProvider();

			Assert.Throws<InvalidOperationException>(() => sut.GetRequiredService<IInterfaceWithAttribute>());
		}

		[Fact]
		public void Herd_WhenAlreadyRegistered_ShouldSkipAutoRegistrationMark()
		{

		    var services = new ServiceCollection();

		    services.AddSingleton<IInterfaceWithAttribute, InterfaceWithAttribute>();

		    services.Scan(x => x.WithOptions(o => o.RegistrationConstraintBehavior =
		                new RegistrationConstraintBehavior()
		                    {AttributeType = typeof(SkipRegistrationTestAttribute)})
		            .WithAssembly<INoImplInterface>());

		    var sut = services.BuildServiceProvider();

			var result = sut.GetService<IInterfaceWithAttribute>();

			Assert.IsType<InterfaceWithAttribute>(result);
		}

		[Fact]
		public void Herd_WhenRequestingCollectionAndOnlyOneImpl_ShouldRegisterIt()
		{
		    var services = new ServiceCollection()
		        .Scan(x =>x.WithAssembly<INoImplInterface>());

		    var container = services.BuildServiceProvider();

			var result = container.GetService<ISingleTypeCollectionHolder>();

			Assert.Single(result.Collection);
		}

	}
}
