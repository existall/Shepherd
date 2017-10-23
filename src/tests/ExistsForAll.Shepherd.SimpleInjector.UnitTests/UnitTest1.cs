using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

			shepherd.Assemblies.AddCompleteTypeAssembly(this.GetType().Assembly);

			var container = shepherd.Herd();
			//container.Verify();

			var allInstances = container.GetInstance<IC>();
		}

		[Fact]
		public void XX()
		{
			var classesImplementingAnInterface = GetClassesImplementingAnInterface(this.GetType().Assembly, typeof(IZx<>));


		}


		///<summary>Read all classes in an assembly that implement an interface (generic, or not generic)</summary>
		//
		// some references
		// return all types implementing an interface
		// http://stackoverflow.com/questions/26733/getting-all-types-that-implement-an-interface/12602220#12602220
		// http://haacked.com/archive/2012/07/23/get-all-types-in-an-assembly.aspx/
		// http://stackoverflow.com/questions/7889228/how-to-prevent-reflectiontypeloadexception-when-calling-assembly-gettypes
		// return all types implementing a generic interface
		// http://stackoverflow.com/questions/33694960/find-all-types-implementing-a-certain-generic-interface-with-specific-t-type
		// http://stackoverflow.com/questions/8645430/get-all-types-implementing-specific-open-generic-type
		// http://stackoverflow.com/questions/1121834/finding-out-if-a-type-implements-a-generic-interface
		// http://stackoverflow.com/questions/5849210/net-getting-all-implementations-of-a-generic-interface
		public static Tuple<bool, IList<Type>> GetClassesImplementingAnInterface(Assembly assemblyToScan,
			Type implementedInterface)
		{
			if (assemblyToScan == null)
				return Tuple.Create(false, (IList<Type>) null);

			if (implementedInterface == null || !implementedInterface.IsInterface)
				return Tuple.Create(false, (IList<Type>) null);

			IEnumerable<Type> typesInTheAssembly;

			try
			{
				typesInTheAssembly = assemblyToScan.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				typesInTheAssembly = e.Types.Where(t => t != null);
			}

			IList<Type> classesImplementingInterface = new List<Type>();

			// if the interface is a generic interface
			if (implementedInterface.IsGenericType)
			{
				foreach (var typeInTheAssembly in typesInTheAssembly)
				{
					if (typeInTheAssembly.IsClass)
					{
						var typeInterfaces = typeInTheAssembly.GetInterfaces();
						foreach (var typeInterface in typeInterfaces)
						{
							if (typeInterface.IsGenericType)
							{
								var typeGenericInterface = typeInterface.GetGenericTypeDefinition();
								var implementedGenericInterface = implementedInterface.GetGenericTypeDefinition();

								if (typeGenericInterface == implementedGenericInterface)
								{
									classesImplementingInterface.Add(typeInTheAssembly);
								}
							}
						}
					}
				}
			}
			else
			{
				foreach (var typeInTheAssembly in typesInTheAssembly)
				{
					if (typeInTheAssembly.IsClass)
					{
						// if the interface is a non-generic interface
						if (implementedInterface.IsAssignableFrom(typeInTheAssembly))
						{
							classesImplementingInterface.Add(typeInTheAssembly);
						}
					}
				}
			}
			return Tuple.Create(true, classesImplementingInterface);
		}


	}


	interface IV<T> : IV
	{

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
	

