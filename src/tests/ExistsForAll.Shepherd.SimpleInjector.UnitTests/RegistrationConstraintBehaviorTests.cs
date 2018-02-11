using System;
using ExistsForAll.Shepherd.SimpleInjector.RegistrationActions;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class RegistrationConstraintBehaviorTests
	{
		[Fact]
		public void ShouldSkipAutoRegistration_WhenInterfaceHasAttribute_ShouldSkipRegistration()
		{
			var sut = BuildSut();

			var result = sut.ShouldSkipAutoRegistration(
				new ServiceDescriptor(typeof(IInterfaceWithAttribute),
					new[] {typeof(InterfaceWithAttribute)})
			);

			Assert.True(result);
		}

		[Fact]
		public void ShouldSkipAutoRegistration_WhenInterfaceDontHaveAttribute_ShouldNotSkipRegistration()
		{
			var sut = BuildSut();

			var result = sut.ShouldSkipAutoRegistration(
				new ServiceDescriptor(typeof(IInterfaceWithOutAttribute),
					new[] {typeof(InterfaceWithOutAttribute)})
			);

			Assert.False(result);
		}

		[Fact]
		public void ShouldSkipAutoRegistration_WhenInterfaceHaveCustomAttribute_ShouldSkipRegistration()
		{
			var sut = BuildSut();

			sut.AttributeType = typeof(SomeAttribute);

			var result = sut.ShouldSkipAutoRegistration(
				new ServiceDescriptor(typeof(IInterfaceWithNewAttribute),
					new[] {typeof(InterfaceWithNewAttribute)})
			);

			Assert.True(result);
		}

		[Fact]
		public void ShouldSkipAutoRegistration_WhenInterfaceDontHaveCustomAttribute_ShouldNotSkipRegistration()
		{
			var sut = BuildSut();

			sut.AttributeType = typeof(SomeAttribute);

			var result = sut.ShouldSkipAutoRegistration(
				new ServiceDescriptor(typeof(IInterfaceWithOutAttribute),
					new[] {typeof(InterfaceWithOutAttribute)})
			);

			Assert.False(result);
		}

		[Fact]
		public void
			ShouldSkipAutoRegistration_WhenInterfaceDontHaveCustomAttributeAndHaveSkipAttribute_ShouldNotSkipRegistration()
		{
			var sut = BuildSut();
			sut.AttributeType = typeof(SomeAttribute);

			var result = sut.ShouldSkipAutoRegistration(
				new ServiceDescriptor(typeof(IInterfaceWithAttribute),
					new[] {typeof(InterfaceWithAttribute)})
			);

			Assert.False(result);
		}

		private static RegistrationConstraintBehavior BuildSut()
		{
			return new RegistrationConstraintBehavior();
		}

		[SkipRegistration]
		private interface IInterfaceWithAttribute
		{
		}

		private class InterfaceWithAttribute : IInterfaceWithAttribute
		{
		}

		private interface IInterfaceWithOutAttribute
		{
		}

		private class InterfaceWithOutAttribute : IInterfaceWithOutAttribute
		{
		}

		private class SomeAttribute : Attribute
		{
		}

		[Some]
		private interface IInterfaceWithNewAttribute
		{
		}

		private class InterfaceWithNewAttribute : IInterfaceWithNewAttribute
		{
		}
	}
}