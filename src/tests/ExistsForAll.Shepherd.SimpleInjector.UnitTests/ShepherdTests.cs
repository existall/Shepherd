using System;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Filters;
using ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects;
using SimpleInjector;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class ShepherdTests
	{
		[Fact]
		public void Herd_FullIntegrationTest()
		{
			var sut = new Shepherd();
			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);
			sut.Options.ServiceIndexer.Filters.Add(new InterfaceAccumulationFilter(typeof(IFilterService)));
			var container = sut.Herd();

			try
			{
				container.Verify();

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

				var openGenericInstance = container.GetInstance(typeof(IOpenGenerics<int>));
				Assert.IsType<OpenGenerics<int>>(openGenericInstance);

				var collectionResults = container.GetAllInstances<ICollectionService>();
				Assert.Collection(collectionResults, i => Assert.IsType<CollectionService1>(i),
					i => Assert.IsType<CollectionService2>(i),
					i => Assert.IsType<CollectionService3>(i));

				NotRegisteredAssertion(typeof(IFilterService));
				NotRegisteredAssertion(typeof(INoImplInterface));
			}
			finally
			{
				container.Dispose();
			}

			void AssertServiceRegistration<T>(Type serviceType, Action<T> assertionAction)
			{
				var instance = (T) container.GetInstance(serviceType);

				assertionAction.Invoke(instance);
			}

			void NotRegisteredAssertion(Type type)
			{
				Assert.Throws<ActivationException>(() => container.GetInstance(type));
			}
		}


		[Fact]
		public void Herd_ShouldSkipAutoRegistrationMark()
		{
			var sut = new Shepherd();
			sut.Options.RegistrationConstraintBehavior =
				new RegistrationConstraintBehavior() {AttributeType = typeof(SkipRegistrationTestAttribute)};
			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);
			var container = sut.Herd();
			
			Assert.Throws<ActivationException>(() => container.GetInstance<IInterfaceWithAttribute>());
		}

		[Fact]
		public void Herd_WhenAlreadyRegistered_ShouldSkipAutoRegistrationMark()
		{
			var container = new Container();
			var sut = new Shepherd(container);

			container.Register<IInterfaceWithAttribute, InterfaceWithAttribute>();
			sut.Options.RegistrationConstraintBehavior =
				new RegistrationConstraintBehavior() {AttributeType = typeof(SkipRegistrationTestAttribute)};
			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);

			container = sut.Herd();

			var result = container.GetInstance<IInterfaceWithAttribute>();
			
			Assert.IsType<InterfaceWithAttribute>(result);
		}

		[Fact]
		public void Herd_FullIntegrationTestWithContainer()
		{
			var container = new Container();

			var sut = new Shepherd(container);
			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);
			sut.Options.ServiceIndexer.Filters.Add(new InterfaceAccumulationFilter(typeof(IFilterService)));
			container = sut.Herd();

			try
			{
				container.Verify();

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

				var openGenericInstance = container.GetInstance(typeof(IOpenGenerics<int>));
				Assert.IsType<OpenGenerics<int>>(openGenericInstance);

				var collectionResults = container.GetAllInstances<ICollectionService>();
				Assert.Collection(collectionResults, i => Assert.IsType<CollectionService1>(i),
					i => Assert.IsType<CollectionService2>(i),
					i => Assert.IsType<CollectionService3>(i));

				Assert.Throws<ActivationException>(() => container.GetInstance<ICollectionService>()); 

				NotRegisteredAssertion(typeof(IFilterService));
				NotRegisteredAssertion(typeof(INoImplInterface));
			}
			finally
			{
				container.Dispose();
			}

			void AssertServiceRegistration<T>(Type serviceType, Action<T> assertionAction)
			{
				var instance = (T) container.GetInstance(serviceType);

				assertionAction.Invoke(instance);
			}

			void NotRegisteredAssertion(Type type)
			{
				Assert.Throws<ActivationException>(() => container.GetInstance(type));
			}
		}
		
		[Fact]
		public void Herd_WhenRequestingCollectionAndOnlyOneImpl_ShouldRegisterIt()
		{
			var sut = new Shepherd();
			
			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);
			
			var container = sut.Herd();
			
			var result = container.GetInstance<ISingleTypeCollectionHolder>();
			
			Assert.Single(result.Collection);
		}

		[Fact]
		public void Herd_WhenRequestingCollectionHasTwoOrMore_ShouldRegisterIt()
		{
			var sut = new Shepherd();

			sut.AddCompleteTypeAssemblies(typeof(INoImplInterface).Assembly);

			var container = sut.Herd();

			var result = container.GetInstance<ISingleTypeCollectionHolder>();

			Assert.Single(result.Collection);
		}
	}
}