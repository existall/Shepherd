using System;
using System.Collections.Generic;
using System.Linq;
using SimpleInjector;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class ResolveUnregisteredTypeSingleImplAsCollectionTests
	{
		[Fact]
		public void AddSingleAsCollectionSupport_WhenResolvingArray_ShouldThrowException()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();

			container.Register<IService, ServiceOne>();
			container.Register<IServicesHolder,ArrayServiceHolder>();

			Assert.Throws<AutoRegistrationException>(() => container.GetInstance<IServicesHolder>());
		}
		
		[Fact]
		public void Test()
		{
			var container = new Container();

			//container.AddSingleAsCollectionSupport();

			container.RegisterCollection(typeof(IService),new [] {typeof(ServiceOne)});
			container.Register<IServicesHolder,ArrayServiceHolder>();

			var servicesHolder = container.GetInstance<IServicesHolder>();
		}
		
		[Fact]
		public void XY()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();
			
			container.Register<IServicesHolder,EnumerableServiceHolder>();

			Assert.Throws<ActivationException>(() => container.GetInstance<IServicesHolder>());
		}

		[Fact]
		public void XYZ()
		{
			var container = new Container();

			container.AddSingleAsCollectionSupport();
			container.Register<IService, ServiceOne>();
			container.Register<IServicesHolder, EnumerableServiceHolder>();

			var result = container.GetInstance<IServicesHolder>();
			
			Assert.Equal(result.IServices.Count(), 1);
		}

		[Fact]
		public void XYZ1()
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