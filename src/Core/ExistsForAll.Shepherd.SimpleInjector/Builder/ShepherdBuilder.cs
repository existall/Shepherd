using System;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Builder
{
	public class ShepherdBuilder
	{
		private Shepherd Shepherd { get; }

		public ShepherdBuilder(Container container)
		{
			Shepherd = new Shepherd(container);
		}

		public ShepherdBuilder WithOptions(Action<IShepherdOptions> action)
		{
			action?.Invoke(Shepherd.Options);
			return this;
		}

		public ShepherdBuilder WithAssemblies(Action<AssemblyCollection> action)
		{
			action?.Invoke(Shepherd.Assemblies);
			return this;
		}

		public ShepherdBuilder WithModules(Action<ModuleCollection> action)
		{
			action?.Invoke(Shepherd.Modules);
			return this;
		}

		public void Herd()
		{
			Shepherd.Herd();
		}
	}
}
