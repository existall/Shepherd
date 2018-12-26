using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public class ServiceCollectionShepherd : Shepherd<IServiceCollection>
	{
		public ServiceCollectionShepherd(IServiceCollection container)
			: base(container)
		{
			Options.Items.AddOrUpdate(Constants.DefaultLifeStyle, ServiceLifetime.Transient);
		}

		public sealed override IShepherdOptions<IServiceCollection> Options { get; protected set; } = new ServiceCollectionOptions();
	}
}