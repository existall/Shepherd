using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Experimental
{
	public class B
	{
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