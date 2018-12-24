using System;
using ExistsForAll.Shepherd.Core;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public static class SimpleInjectorExtensions
	{
		public static Container Scan(this Container target, Action<ShepherdBuilder<Container>> action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			var shepherd = new SimpleInjectorShepherd(target);

			var builder = new ShepherdBuilder<Container>(shepherd);

			action(builder);

			shepherd.Herd();

			return target;
		}
	}
}