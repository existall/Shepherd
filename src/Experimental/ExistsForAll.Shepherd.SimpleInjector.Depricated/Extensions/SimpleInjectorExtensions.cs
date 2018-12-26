using System;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Builder;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions
{
	public static class SimpleInjectorExtensions
	{
		public static Container Scan(this Container target, Action<ShepherdBuilder> action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			var shepherd = new Shepherd(target);

			var builder = new ShepherdBuilder(shepherd);

			action(builder);

			shepherd.Herd();

			return target;
		}
	}
}