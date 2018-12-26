using ExistsForAll.Shepherd.Core.Extensions;
using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public static class RegistrationContextExtensions
	{
		public static ServiceLifetime GetDefaultLifeStyle(this IRegistrationContext target)
		{
			if (target.Properties.TryGetValue(Constants.DefaultLifeStyle, out var lifetime))
				return (ServiceLifetime)lifetime;

			return ServiceLifetime.Transient;
		}
	}
}