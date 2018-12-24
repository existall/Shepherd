using ExistsForAll.Shepherd.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class ServiceCollectionShepherd : Shepherd<IServiceCollection>
	{
		public ServiceCollectionShepherd(IServiceCollection container) 
			: base(container)
		{
		}

		public override IShepherdOptions<IServiceCollection> Options { get; protected set; } = new ServiceCollectionOptions();
	}
}