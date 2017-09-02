namespace ExistAll.Shepherd.Core
{
	public interface IModule
	{
		void Run(IRegistrationContext context);
	}

	public interface IModule<in T> : IModule
	{
		void Something(IRegistrationContext<T> action);
	}
}