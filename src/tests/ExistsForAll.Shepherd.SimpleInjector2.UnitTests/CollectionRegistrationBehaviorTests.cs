using System;
using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.SimpleInjector._2;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector2.UnitTests
{
	public class CollectionRegistrationBehaviorTests
	{
		[Fact]
		public void ShouldRegister_WhenHaveLessThanTwoImpl_ShouldReturnFalse()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(TestUtils.GetType<Interface>());

			var result = sut.ShouldRegister(descriptor);
			Assert.False(result);
		}

		[Fact]
		public void ShouldRegister_WhenHaveMoreThanOneImpl_ShouldReturnFalse()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(TestUtils.GetType<Interface>(), TestUtils.GetType<Interface1>());

			var result = sut.ShouldRegister(descriptor);
			Assert.True(result);
		}

		private IServiceTypeMap BuildDescriptor(Type type, params Type[] types)
		{
			return TestUtils.BuildServiceDescriptor(TestUtils.GetType<IInterface>(), type, types);
		}

		private static CollectionRegistrationBehavior BuildSut()
		{
			return new CollectionRegistrationBehavior();
		}

		private interface IInterface
		{

		}

		private class Interface : IInterface
		{
		}

		private class Interface1 : IInterface
		{
		}
	}
}