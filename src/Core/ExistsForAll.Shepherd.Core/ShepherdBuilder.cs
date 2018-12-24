using System;
using System.Reflection;
using ExistsForAll.Shepherd.Core.Extensions;
using ExistsForAll.Shepherd.Core.Filters;
using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public class ShepherdBuilder<TContainer>
	{
		private Shepherd<TContainer> Shepherd { get; }

		public ShepherdBuilder(Shepherd<TContainer> shepherd)
		{
			Shepherd = shepherd;
		}

		public ShepherdBuilder<TContainer> WithOptions(Action<IShepherdOptions<TContainer>> action)
		{
			action?.Invoke(Shepherd.Options);
			return this;
		}

		public ShepherdBuilder<TContainer> WithAssemblies(Action<AssemblyCollection> action)
		{
			action?.Invoke(Shepherd.Assemblies);
			return this;
		}

		public ShepherdBuilder<TContainer> WithModules(Action<ModuleCollection<TContainer>> action)
		{
			action?.Invoke(Shepherd.Modules);
			return this;
		}

		public ShepherdBuilder<TContainer> WithAssembly<T>()
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			WithAssemblies(x => x.AddAssemblies(assembly));

			return this;
		}

		public ShepherdBuilder<TContainer> WithModule(IModule<TContainer> module)
		{
			WithModules(x => x.AddModule(module));
			return this;
		}
		
		public ShepherdBuilder<TContainer> UseServiceIndexer(IServiceIndexer serviceIndexer)
		{
			WithOptions(x => x.ServiceIndexer = serviceIndexer);

			return this;
		}

		public ShepherdBuilder<TContainer> UseServiceIndexerFilter(IFilter filter)
		{
			WithOptions(x => x.ServiceIndexer.Filters.Add(filter));

			return this;
		}

		public ShepherdBuilder<TContainer> UseCollectionRegistrationBehavior(ICollectionRegistrationBehavior<TContainer> behavior)
		{
			WithOptions(x => x.CollectionRegistrationBehavior = behavior);

			return this;
		}

		public ShepherdBuilder<TContainer> UseDecoratorRegistrationBehavior(IDecoratorRegistrationBehavior<TContainer> behavior)
		{
			WithOptions(x => x.DecoratorRegistrationBehavior = behavior);

			return this;
		}

		public ShepherdBuilder<TContainer> UseGenericRegistrationBehavior(IGenericRegistrationBehavior<TContainer> behavior)
		{
			WithOptions(x => x.GenericRegistrationBehavior = behavior);

			return this;
		}

		public ShepherdBuilder<TContainer> UseRegistrationConstraintBehavior(IRegistrationConstraintBehavior behavior)
		{
			WithOptions(x => x.RegistrationConstraintBehavior = behavior);

			return this;
		}

		public ShepherdBuilder<TContainer> UseSingleServiceRegistrationBehavior(ISingleServiceRegistrationBehavior<TContainer> behavior)
		{
			WithOptions(x => x.SingleServiceRegistrationBehavior = behavior);

			return this;
		}
	}
}
