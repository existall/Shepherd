using System.Collections.Generic;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using Xunit;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var shepherd = new Shepherd();

			shepherd.Assemblies.AddAllTypeAssemblies(this.GetType().Assembly);

			var container = shepherd.Herd();
			//container.Verify();

			var allInstances = container.GetInstance<IC>();
		}


		interface IV
		{
			
		}

		interface IV<T> : IV
		{
			
		}

		class V : IV<int>
		{
			
		}

		class V1 : IV<string>
		{
			
		}


		interface IZx<T>
		{
			
		}

		private class Zx<T> : IZx<T>
		{
		}


		interface IC
		{
		
		}

		class C : IC
		{
			private readonly IZx<int> _z;
			private readonly IEnumerable<IV> _enumerable;

			public C(IZx<int> z, IEnumerable<IV> enumerable)
			{
				_z = z;
				_enumerable = enumerable;

				foreach (var x in _enumerable)
				{
					
				}
			}
		}

		//interface IZ
		//{

		//}

		//class Zx : IZ
		//{
		//}

		//public interface IX
		//{

		//}

		//class X1 : IX
		//{

		//}

		//class X : IX
		//{
		//}

		//interface IY
		//{


		//}

		//class Y : IY
		//{
		//	private readonly IEnumerable<IX> _x;

		//	public Y(IEnumerable<IX> x)
		//	{
		//		_x = x;
		//	}
		//}
	}
}
