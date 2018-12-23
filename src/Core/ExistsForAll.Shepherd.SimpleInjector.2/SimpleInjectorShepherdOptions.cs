using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector._2
{
	public class SimpleInjectorShepherdOptions : ShepherdOptions<Container>
	{
		public override IGenericRegistrationBehavior<Container> GenericRegistrationBehavior { get; set; } =
			new GenericRegistrationBehavior();

		public override IDecoratorRegistrationBehavior<Container> DecoratorRegistrationBehavior { get; set; } =
			new DecoratorRegistrationBehavior();

		public override ICollectionRegistrationBehavior<Container> CollectionRegistrationBehavior { get; set; } =
			new CollectionRegistrationBehavior();

		public override ISingleServiceRegistrationBehavior<Container> SingleServiceRegistrationBehavior { get; set; } =
			new SingleServiceRegistrationBehavior();
	}
}