using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public interface IShepherdOptions<TContainer>
	{
		IServiceIndexer ServiceIndexer { get; set; }
		IRegistrationConstraintBehavior RegistrationConstraintBehavior { get; set; }
		IGenericRegistrationBehavior<TContainer> GenericRegistrationBehavior { get; set; }
		IDecoratorRegistrationBehavior<TContainer> DecoratorRegistrationBehavior { get; set; }
		ICollectionRegistrationBehavior<TContainer> CollectionRegistrationBehavior { get; set; }
		ISingleServiceRegistrationBehavior<TContainer> SingleServiceRegistrationBehavior { get; set; }
	}
}