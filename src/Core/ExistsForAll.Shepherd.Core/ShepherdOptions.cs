using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public class ShepherdOptions<TContainer> : IShepherdOptions<TContainer>
	{
		public IServiceIndexer ServiceIndexer { get; set; } = new ServiceIndexer();
		public IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; } = new RegistrationConstraintBehavior();
		public IGenericRegistrationBehavior<TContainer> GenericRegistrationBehavior { get; set; }
		public IDecoratorRegistrationBehavior<TContainer> DecoratorRegistrationBehavior { get; set; }
		public ICollectionRegistrationBehavior<TContainer> CollectionRegistrationBehavior { get; set; }
		public ISingleServiceRegistrationBehavior<TContainer> SingleServiceRegistrationBehavior { get; set; }
	}
}