using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public abstract class ShepherdOptions<TContainer> : IShepherdOptions<TContainer>
	{
		public IServiceIndexer ServiceIndexer { get; set; } = new ServiceIndexer();
		public IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; } = new RegistrationConstraintBehavior();
		public abstract IGenericRegistrationBehavior<TContainer> GenericRegistrationBehavior { get; set; }
		public abstract IDecoratorRegistrationBehavior<TContainer> DecoratorRegistrationBehavior { get; set; }
		public abstract ICollectionRegistrationBehavior<TContainer> CollectionRegistrationBehavior { get; set; }
		public abstract ISingleServiceRegistrationBehavior<TContainer> SingleServiceRegistrationBehavior { get; set; }
	}
}