using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IContainerOptionsConfiguration
	{
		void Configure(ContainerOptions containerOptions);
	}
}