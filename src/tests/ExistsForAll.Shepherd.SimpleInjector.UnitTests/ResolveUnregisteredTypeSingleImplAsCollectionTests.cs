using System.Collections.Generic;
using SimpleInjector;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class ResolveUnregisteredTypeSingleImplAsCollectionTests
	{
		[Fact]
		public void AddSingleAsCollectionSupport_WhenResolvingArray_ShouldResolve()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();

			container.Register<IService, ServiceOne>();
			container.Register<IServicesHolder,ArrayServiceHolder>();

			var result = container.GetInstance<IServicesHolder>();

			Assert.Single(result.IServices);
		}
		
		[Fact]
		public void AddSingleAsCollectionSupport_WhenResolvingWithoutRegisteringDependency_ShouldThrowSimpleInjectorException()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();
			
			container.Register<IServicesHolder,EnumerableServiceHolder>();

			Assert.Throws<ActivationException>(() => container.GetInstance<IServicesHolder>());
		}

		[Fact]
		public void AddSingleAsCollectionSupport_WhenResolvingStream_ShouldResolve()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();
			container.Register<IService, ServiceOne>();
			container.Register<IServicesHolder, EnumerableServiceHolder>();

			var result = container.GetInstance<IServicesHolder>();
			
			Assert.Single(result.IServices);
		}

		[Fact]
		public void AddSingleAsCollectionSupport_WhenRegistering_ShouldHaveTheSameLifestyle()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();
			container.RegisterSingleton<IService, ServiceOne>();
			container.RegisterSingleton<IServicesHolder, EnumerableServiceHolder>();

			var result = container.GetRegistration(typeof(IEnumerable<IService>));
			
			Assert.Equal(result.Lifestyle, Lifestyle.Singleton);
		}


		private interface IService
		{
			
		}

		private class ServiceOne : IService
		{
			
		}
		
		private interface IServicesHolder
		{
			IEnumerable<IService> IServices { get; }
		}

		private class ArrayServiceHolder : IServicesHolder
		{
			public IEnumerable<IService> IServices { get; }

			public ArrayServiceHolder(IService[] services)
			{
				IServices = services;
			}
		}
		
		private class EnumerableServiceHolder : IServicesHolder
		{
			public IEnumerable<IService> IServices { get; }

			public EnumerableServiceHolder(IEnumerable<IService> services)
			{
				IServices = services;
			}
		}
	}
}