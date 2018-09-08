using System;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Builder
{
	public class ShepherdBuilder : IOptionsBuilder, IAssembliesBuilder
	{
		private Shepherd Shepherd { get; }

		public ShepherdBuilder(Container container)
		{
			Shepherd = new Shepherd(container);
		}

		public object WithOptions(Action<IShepherdOptions> action)
		{
			action?.Invoke(Shepherd.Options);
			return this;
		}

		public object WithAssemblies(Action<AssemblyCollection> action)
		{
			action?.Invoke(Shepherd.Assemblies);
			return this;
		}
	}

	public interface IAssembliesBuilder
	{
	}

	public interface IOptionsBuilder
	{
		object WithOptions(Action<IShepherdOptions> action);
	}
}