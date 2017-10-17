using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IContainerOptionsConfiguration
	{
		void Configure(ContainerOptions containerOptions);
	}
}