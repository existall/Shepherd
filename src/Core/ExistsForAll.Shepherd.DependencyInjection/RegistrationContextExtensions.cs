using ExistsForAll.Shepherd.Core.RegistrationActions;
using Microsoft.Extensions.DependencyInjection;

namespace ExistsForAll.Shepherd.DependencyInjection
{
	public static class RegistrationContextExtensions
	{
		private const string DefaultLifeStyle = "DefaultLifeStyle";
		
		public static IRegistrationContext SetDefaultLifeStyle(this IRegistrationContext target, ServiceLifetime lifetime)
		{
			if (target.Properties.ContainsKey(DefaultLifeStyle))
				target.Properties[DefaultLifeStyle] = lifetime;
			else    
				target.Properties.Add(DefaultLifeStyle, lifetime);

			return target;
		}

		public static ServiceLifetime GetDefaultLifeStyle(this IRegistrationContext target)
		{
			if (target.Properties.TryGetValue(DefaultLifeStyle, out var lifetime))
				return (ServiceLifetime)lifetime;
			
			return ServiceLifetime.Transient;
		}
	}
}