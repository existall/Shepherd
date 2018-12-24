using ExistsForAll.Shepherd.Core;
using ExistsForAll.Shepherd.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public static class ServiceCollectionBuilderExtensions
	{
		public static ShepherdBuilder<IServiceCollection> SetDefaultLifetime(this ShepherdBuilder<IServiceCollection> target, ServiceLifetime serviceLifetime)
		{
			target.WithOptions(x => x.Items.AddOrUpdate(RegistrationContextExtensions.DefaultLifeStyle, serviceLifetime));
			return target;
		}
	}
}