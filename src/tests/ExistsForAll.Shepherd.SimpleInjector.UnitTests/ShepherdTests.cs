using System;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Filters;
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
			var container = new Container()
				.Scan(x => x.WithAssembly<INoImplInterface>()
					.UseServiceIndexerFilter(new InterfaceAccumulationFilter(typeof(IFilterService))));

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
			var container = new Container();
			var sut = new SimpleInjectorShepherd(container);
			sut.Options.RegistrationConstraintBehavior =
				new RegistrationConstraintBehavior() {AttributeType = typeof(SkipRegistrationTestAttribute)};
			sut.Assemblies.Add(new AssemblyLoader(typeof(INoImplInterface).Assembly));
			sut.Herd();

			Assert.Throws<ActivationException>(() => container.GetInstance<IInterfaceWithAttribute>());
		}

		[Fact]
		public void Herd_WhenAlreadyRegistered_ShouldSkipAutoRegistrationMark()
		{
			var container = new Container();
			var sut = new SimpleInjectorShepherd(container);

			container.Register<IInterfaceWithAttribute, InterfaceWithAttribute>();
			sut.Options.RegistrationConstraintBehavior =
				new RegistrationConstraintBehavior() {AttributeType = typeof(SkipRegistrationTestAttribute)};
			sut.Assemblies.Add(new AssemblyLoader(typeof(INoImplInterface).Assembly));
			sut.Herd();

			var result = container.GetInstance<IInterfaceWithAttribute>();

			Assert.IsType<InterfaceWithAttribute>(result);
		}

		[Fact]
		public void Herd_FullIntegrationTestWithContainer()
		{
			var container = new Container();

			var sut = new SimpleInjectorShepherd(container);
			sut.Assemblies.Add(new AssemblyLoader(typeof(INoImplInterface).Assembly));
			sut.Options.ServiceIndexer.Filters.Add(new InterfaceAccumulationFilter(typeof(IFilterService)));
			sut.Herd();

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
			var container = new Container();
			container.AddSingleAsCollectionSupport();
			var sut = new SimpleInjectorShepherd(container);

			sut.Assemblies.Add(new AssemblyLoader(typeof(INoImplInterface).Assembly));

			sut.Herd();

			var result = container.GetInstance<ISingleTypeCollectionHolder>();

			Assert.Single(result.Collection);
		}

		[Fact]
		public void Herd_WhenRequestingCollectionHasTwoOrMore_ShouldRegisterIt()
		{
			var container = new Container();
			var sut = new SimpleInjectorShepherd(container);

			sut.Assemblies.Add(new AssemblyLoader(typeof(INoImplInterface).Assembly));

			sut.Herd();

			var result = container.GetInstance<ISingleTypeCollectionHolder>();

			Assert.Single(result.Collection);
		}

		[Fact]
		public void Herd_WhenPassingProperties_ShouldBeAvailableInContext()
		{
			var container = new Container()
				.Scan(x => x.WithAssembly<INoImplInterface>()
					.AddOrUpdateProperty(TestModule.PropertyName, TestModule.PropertyValue)
					.WithModule(new TestModule()));
		}

		private class TestModule : IModule<Container>
		{
			public const string PropertyName = "some-name";
			public static readonly Guid PropertyValue = Guid.NewGuid();

			public void Configure(IModuleContext<Container> context)
			{
				context.Properties.TryGetValue(PropertyName, out var value);
				Assert.Equal(PropertyValue, value);
			}
		}
	}
}