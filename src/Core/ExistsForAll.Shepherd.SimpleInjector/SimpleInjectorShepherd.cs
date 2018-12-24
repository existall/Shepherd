using ExistsForAll.Shepherd.Core;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class SimpleInjectorShepherd : Shepherd<Container>
	{
		public SimpleInjectorShepherd(Container container)
			: base(container)
		{
			container.AddSingleAsCollectionSupport();
		}

		public override IShepherdOptions<Container> Options { get; protected set; } =
			new SimpleInjectorShepherdOptions();
	}
}