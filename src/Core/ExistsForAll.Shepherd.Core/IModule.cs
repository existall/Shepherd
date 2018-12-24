namespace ExistsForAll.Shepherd.Core
{
	public interface IModule<in TContainer>
	{
		void Configure(IModuleContext<TContainer> context);
	}
}