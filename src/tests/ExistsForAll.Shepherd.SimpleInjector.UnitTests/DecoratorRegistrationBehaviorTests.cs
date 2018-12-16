using System;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using Xunit;
using static ExistsForAll.Shepherd.SimpleInjector.UnitTests.TestUtils;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class DecoratorRegistrationBehaviorTests
	{
		[Fact]
		public void ShouldRegister_WhenImplDontHaveDecorator_ShouldNotRegister()
		{
			var sut = BuildSut();

			var serviceDescriptor = BuildDescriptor(GetType<Interface>());

			var result = sut.ShouldRegister(serviceDescriptor);

			Assert.False(result);
		}

		[Fact]
		public void ShouldRegister_WhenMoreThanOneImplDontHaveDecorator_ShouldNotRegister()
		{
			var sut = BuildSut();

			var serviceDescriptor = BuildDescriptor(GetType<Interface>(), GetType<AnotherInterface>());

			var result = sut.ShouldRegister(serviceDescriptor);

			Assert.False(result);
		}

		[Fact]
		public void ShouldRegister_WhenHaveDecorator_ShouldReturnTrue()
		{
			var sut = BuildSut();

			var serviceDescriptor = BuildDescriptor(GetType<Interface>(), GetType<Decorator>());

			var result = sut.ShouldRegister(serviceDescriptor);

			Assert.True(result);
		}

		[Fact]
		public void ShouldRegister_WhenHaveDecoratorAndMoresThanTwoImpl_ShouldThrowException()
		{
			var sut = BuildSut();

			var serviceDescriptor = BuildDescriptor(GetType<Interface>(), GetType<Decorator>(), GetType<AnotherInterface>());

			Assert.Throws<DecoratorRegistrationException>(() => sut.ShouldRegister(serviceDescriptor));
		}

		private static DecoratorRegistrationBehavior BuildSut()
		{
			return new DecoratorRegistrationBehavior();
		}

		private interface IInterface
		{

		}

		private class Interface : IInterface
		{
		}

		private class AnotherInterface : IInterface
		{

		}

		private class Decorator : IInterface
		{
			private readonly IInterface _interface;

			public Decorator(IInterface @interface)
			{
				_interface = @interface;
			}
		}

		private IServiceTypeMap BuildDescriptor(Type type, params Type[] types)
		{
			return TestUtils.BuildServiceDescriptor(typeof(IInterface), type, types);
		}
	}
}
