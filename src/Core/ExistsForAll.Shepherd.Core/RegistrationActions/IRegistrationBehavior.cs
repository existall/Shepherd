namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	public interface IRegistrationBehavior<in TContainer>
	{
		bool ShouldRegister(IServiceTypeMap typeMap);
		void Register(IRegistrationContext<TContainer> context);
	}
}