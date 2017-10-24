using System;
using System.Linq;
using ExistsForAll.Shepherd.SimpleInjector.Filters;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public partial class ServiceIndexerTests
	{
		[Fact]
		public void MapTypes_WhenHaveOneInterfaceAndOneImpl_ShouldMapInterfaceToClass()
		{
			var sut = BuildSut();

			var result = sut.MapTypes(new[] { GetType<IInterface>(), GetType<Interface>() });

			var map = result.Single();

			Assert.Equal(typeof(IInterface), map.ServiceType);
			Assert.Single(map.ImplementationTypes);
			Assert.Equal(typeof(Interface), map.ImplementationTypes.First());
		}

		[Fact]
		public void MapTypes_WhenHaveOneInterfaceAndOneImplWithFilter_ShouldReturnEmptyMapping()
		{
			var sut = BuildSut();
			sut.Filters.Add(new InterfaceAccumulationFilter(typeof(IInterface)));

			var result = sut.MapTypes(new[] { GetType<IInterface>(), GetType<Interface>() });

			Assert.Empty(result);
		}

		[Fact]
		public void MapTypes_WhenHaveTwoInterfaceWithFilter_ShouldReturnNotFiltered()
		{
			var sut = BuildSut();
			sut.Filters.Add(new InterfaceAccumulationFilter(typeof(IInterface)));

			var result = sut.MapTypes(new[] { GetType<IInterface>(),
				GetType<Interface>(),
				GetType<IInterface2>(),
				GetType<Interface2>()
			});

			var map = result.Single();

			Assert.Equal(typeof(IInterface2), map.ServiceType);
			Assert.Single(map.ImplementationTypes);
			Assert.Equal(typeof(Interface2), map.ImplementationTypes.First());
		}

		[Fact]
		public void MapTypes_WhenHaveOneInterfaceAndOneImplWithClassFilter_ShouldExcludeImpl()
		{
			var sut = BuildSut();
			sut.Filters.Add(new ImplementationAccumulationFilter(typeof(Interface)));

			var result = sut.MapTypes(new[] { GetType<IInterface>(),
				GetType<Interface>(),
				GetType<IInterface2>(),
				GetType<Interface2>()
			});

			var map = result
				.Single(x => x.ServiceType == typeof(IInterface));

			Assert.Empty(map.ImplementationTypes);
		}

		[Fact]
		public void MapTypes_WhenHasOpenGenericType_ShouldMapThem()
		{
			var sut = BuildSut();

			var results = sut.MapTypes(new[] { typeof(IOpenGenericsInterface<>), typeof(OpenGenericsInterface<>) });

			var map = results.Single(x => x.ServiceType == typeof(IOpenGenericsInterface<>));

			Assert.Single(map.ImplementationTypes, typeof(OpenGenericsInterface<>));
		}

		[Fact]
		public void MapTypes_WhenHasOpenCloseGenericType_ShouldMapThem()
		{
			var sut = BuildSut();

			var results = sut.MapTypes(new[] { typeof(IOpenCloseGenericsInterface<>),
				GetType<IntOpenCloseGenericsInterface>(),
				GetType<StringOpenCloseGenericsInterface>()
			});

			var map = results.Single(x => x.ServiceType == typeof(IOpenCloseGenericsInterface<>));

			Assert.Collection(map.ImplementationTypes,
				item => Assert.Equal(typeof(IntOpenCloseGenericsInterface), item),
				item => Assert.Equal(typeof(StringOpenCloseGenericsInterface), item));
		}

		[Fact]
		public void MapTypes_WhenIndexInterfaceWithoutImpl_ShouldNotHaveImp()
		{
			var sut = BuildSut();

			var results = sut.MapTypes(new[] { typeof(IInterface) });

			var map = results.Single(x => x.ServiceType == typeof(IInterface));

			Assert.Empty(map.ImplementationTypes);
		}

		[Fact]
		public void MapTypes_WhenTryingToIndexAllTypes_ShouldIndexAll()
		{
			var sut = BuildSut();

			var results = sut.MapTypes(new[] {
				GetType<IInterface>(),
				GetType<Interface>(),
				GetType<IInterface2>(),
				GetType<Interface2>(),
				typeof(IOpenGenericsInterface<>),
				typeof(OpenGenericsInterface<>),
				typeof(IOpenCloseGenericsInterface<>),
				GetType<IntOpenCloseGenericsInterface>(),
				GetType<StringOpenCloseGenericsInterface>()
			}).ToArray();


			Assert.NotEmpty(results);

			AssertMap(results, GetType<IInterface>(), new[] { GetType<Interface>() });
			AssertMap(results, GetType<IInterface2>(), new[] { GetType<Interface2>() });
			AssertMap(results, typeof(IOpenGenericsInterface<>), new[] { typeof(OpenGenericsInterface<>) });
			AssertMap(results, typeof(IOpenCloseGenericsInterface<>), new[] {
					GetType<IntOpenCloseGenericsInterface>(),
					GetType<StringOpenCloseGenericsInterface>() });
		}

		private static void AssertMap(ServiceTypeMap[] mapping, Type serviceType, Type[] implementationTypes)
		{
			var map = mapping.Single(x => x.ServiceType == serviceType);
			Assert.Equal(map.ServiceType, serviceType);
			Assert.Equal(map.ImplementationTypes, implementationTypes);
		}

		private static ServiceIndexer BuildSut()
		{
			return new ServiceIndexer();
		}

		private static Type GetType<T>()
		{
			return typeof(T);
		}
	}


	public partial class ServiceIndexerTests
	{
		private interface IInterface
		{

		}

		private class Interface : IInterface
		{
		}

		private interface IInterface2
		{

		}

		private class Interface2 : IInterface2
		{
		}

		private interface IOpenGenericsInterface<T>
		{

		}

		private class OpenGenericsInterface<T> : IOpenGenericsInterface<T>
		{
		}

		private interface IOpenCloseGenericsInterface<T>
		{

		}

		private class IntOpenCloseGenericsInterface : IOpenCloseGenericsInterface<int>
		{
		}

		private class StringOpenCloseGenericsInterface : IOpenCloseGenericsInterface<string>
		{
		}
	}
}
