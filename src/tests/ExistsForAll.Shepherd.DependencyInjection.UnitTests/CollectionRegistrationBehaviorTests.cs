using System;
using ExistsForAll.Shepherd.Core;
using Xunit;

namespace ExistsForAll.Shepherd.DependencyInjection.UnitTests
{
    public class CollectionRegistrationBehaviorTests
    {
        [Fact]
        public void ShouldRegister_WhenHaveLessThanTwoImpl_ShouldReturnFalse()
        {
            var sut = BuildSut();
            var descriptor = BuildServiceMap(TestUtils.GetType<Interface>());

            var result = sut.ShouldRegister(descriptor);
            Assert.False(result);
        }

        [Fact]
        public void ShouldRegister_WhenHaveMoreThanOneImpl_ShouldReturnFalse()
        {
            var sut = BuildSut();
            var descriptor = BuildServiceMap(TestUtils.GetType<Interface>(), TestUtils.GetType<Interface1>());

            var result = sut.ShouldRegister(descriptor);
            Assert.True(result);
        }

        private IServiceTypeMap BuildServiceMap(Type type, params Type[] types)
        {
            return TestUtils.BuildServiceMap(TestUtils.GetType<IInterface>(), type, types);
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
