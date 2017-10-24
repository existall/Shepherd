using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Experimental
{
	public class B
	{
		public static Tuple<bool, IList<Type>> GetClassesImplementingAnInterface(Assembly assemblyToScan,
			Type implementedInterface)
		{
			if (assemblyToScan == null)
				return Tuple.Create(false, (IList<Type>)null);

			if (implementedInterface == null || !implementedInterface.IsInterface)
				return Tuple.Create(false, (IList<Type>)null);

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

		public void Z()
		{
			var container = new Container();

			container.ResolveUnregisteredType += (x, y) =>
			{

			};

			var fieldInfos = typeof(Container).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			var eventsField = typeof(Container).GetField("resolveUnregisteredType", BindingFlags.NonPublic | BindingFlags.Instance);

			var value = eventsField.GetValue(container);
			var eventHandlerList = eventsField.GetValue(container);

			var copy = container.C();

			//container.Register<IX, X>();

			eventsField.SetValue(copy, eventHandlerList);

			//copy.GetInstance<IX>();

			//eventsField.SetValue(button2, eventHandlerList);

			//var instance1 = container.GetInstance<IZ>();

			//var deepCopy = container.Copy();

			//deepCopy.ResolveUnregisteredType += container.ResolveUnregisteredType;


			var t = container.GetType().GetField("ResolveUnregisteredType",
				BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);

			var eventInfo = container.GetType().GetEvent("ResolveUnregisteredType");
			//Dim addDelegate As[Delegate] = sourceDelegate.GetInvocationList().First() ' if its multicast, then you'll need to copy the lot
			//var deepCopy = Mapper.Map<Container, Container>(container);
			//AddHandler destObject.SomeEvent, addDelegate

			//var instance = deepCopy.GetInstance<IZ>();

		}
	}
}