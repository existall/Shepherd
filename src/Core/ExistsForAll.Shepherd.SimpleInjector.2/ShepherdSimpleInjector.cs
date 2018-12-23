using ExistsForAll.Shepherd.Core;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector._2
{
	public class ShepherdSimpleInjector : Shepherd<Container>
	{
		public ShepherdSimpleInjector(Container container)
			: base(container)
		{
		}

		public override IShepherdOptions<Container> Options { get; protected set; } =
			new SimpleInjectorShepherdOptions();
	}
}