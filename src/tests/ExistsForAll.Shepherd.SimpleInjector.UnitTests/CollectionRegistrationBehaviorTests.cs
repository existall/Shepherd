using System;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;
using Xunit;
using static ExistsForAll.Shepherd.SimpleInjector.Depricated.UnitTests.TestUtils;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class CollectionRegistrationBehaviorTests
	{
		[Fact]
		public void ShouldRegister_WhenHaveLessThanTwoImpl_ShouldReturnFalse()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(GetType<Interface>());

			var result = sut.ShouldRegister(descriptor);
			Assert.False(result);
		}

		[Fact]
		public void ShouldRegister_WhenHaveMoreThanOneImpl_ShouldReturnFalse()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(GetType<Interface>(), GetType<Interface1>());

			var result = sut.ShouldRegister(descriptor);
			Assert.True(result);
		}

		private IServiceTypeMap BuildDescriptor(Type type, params Type[] types)
		{
			return BuildServiceDescriptor(GetType<IInterface>(), type, types);
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