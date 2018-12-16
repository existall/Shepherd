using System;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class SingleServiceRegistrationBehaviorTests
	{
		[Fact]
		public void ShouldRegister_WhenHaveOneImpl_ShouldReturnTrue()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(TestUtils.GetType<Interface>());

			var result = sut.ShouldRegister(descriptor);
			Assert.True(result);
		}

		[Fact]
		public void ShouldRegister_WhenHaveMoreThanOneImpl_ShouldReturnFalse()
		{
			var sut = BuildSut();
			var descriptor = BuildDescriptor(TestUtils.GetType<Interface>(), TestUtils.GetType<Interface1>());

			var result = sut.ShouldRegister(descriptor);
			Assert.False(result);
		}

		private IServiceTypeMap BuildDescriptor(Type type, params Type[] types)
		{
			return TestUtils.BuildServiceDescriptor(TestUtils.GetType<IInterface>(), type, types);
		}

		private static SingleServiceRegistrationBehavior BuildSut()
		{
			return new SingleServiceRegistrationBehavior();
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