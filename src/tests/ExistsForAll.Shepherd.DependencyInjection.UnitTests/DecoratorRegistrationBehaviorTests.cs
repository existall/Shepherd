using System;
using ExistsForAll.Shepherd.Core;
using Xunit;

namespace ExistsForAll.Shepherd.DependencyInjection.UnitTests
{
    public class DecoratorRegistrationBehaviorTests
    {
        [Fact]
        public void ShouldRegister_WhenImplDontHaveDecorator_ShouldNotRegister()
        {
            var sut = BuildSut();

            var serviceMap = BuildDescriptor(TestUtils.GetType<Interface>());

            var result = sut.ShouldRegister(serviceMap);

            Assert.False(result);
        }

        [Fact]
        public void ShouldRegister_WhenMoreThanOneImplDontHaveDecorator_ShouldNotRegister()
        {
            var sut = BuildSut();

            var serviceMap = BuildDescriptor(TestUtils.GetType<Interface>(), TestUtils.GetType<AnotherInterface>());

            var result = sut.ShouldRegister(serviceMap);

            Assert.False(result);
        }

        [Fact]
        public void ShouldRegister_WhenHaveDecorator_ShouldReturnTrue()
        {
            var sut = BuildSut();

            var serviceMap = BuildDescriptor(TestUtils.GetType<Interface>(), TestUtils.GetType<Decorator>());

            var result = sut.ShouldRegister(serviceMap);

            Assert.True(result);
        }

        [Fact]
        public void ShouldRegister_WhenHaveDecoratorAndMoresThanTwoImpl_ShouldThrowException()
        {
            var sut = BuildSut();

            var serviceMap = BuildDescriptor(TestUtils.GetType<Interface>(), TestUtils.GetType<Decorator>(), TestUtils.GetType<AnotherInterface>());

            Assert.Throws<DecoratorRegistrationException>(() => sut.ShouldRegister(serviceMap));
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
            return TestUtils.BuildServiceMap(typeof(IInterface), type, types);
        }
    }
}
