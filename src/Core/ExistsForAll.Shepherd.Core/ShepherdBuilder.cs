using System;
using System.Reflection;
using ExistsForAll.Shepherd.Core.Extensions;
using ExistsForAll.Shepherd.Core.Filters;
using ExistsForAll.Shepherd.Core.RegistrationActions;

namespace ExistsForAll.Shepherd.Core
{
	public static class ShepherdBuilderExtensions
	{
		public static ShepherdBuilder<TContainer> WithAssembly<TContainer, T>(this ShepherdBuilder<TContainer> target)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			target.WithAssemblies(x => x.AddAssemblies(assembly));

			return target;
		}
		

		public static ShepherdBuilder<TContainer> UseServiceIndexer<TContainer>(this ShepherdBuilder<TContainer> target, IServiceIndexer serviceIndexer)
		{
			target.WithOptions(x => x.ServiceIndexer = serviceIndexer);

			return target;
		}

		public static ShepherdBuilder<TContainer> UseServiceIndexerFilter<TContainer>(this ShepherdBuilder<TContainer> target, IFilter filter)
		{
			target.WithOptions(x => x.ServiceIndexer.Filters.Add(filter));

			return target;
		}

		public static ShepherdBuilder<TContainer> UseCollectionRegistrationBehavior<TContainer>(this ShepherdBuilder<TContainer> target,
			ICollectionRegistrationBehavior<TContainer> behavior)
		{
			target.WithOptions(x => x.CollectionRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder<TContainer> UseDecoratorRegistrationBehavior<TContainer>(this ShepherdBuilder<TContainer> target,
			IDecoratorRegistrationBehavior<TContainer> behavior)
		{
			target.WithOptions(x => x.DecoratorRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder<TContainer> UseGenericRegistrationBehavior<TContainer>(this ShepherdBuilder<TContainer> target,
			IGenericRegistrationBehavior<TContainer> behavior)
		{
			target.WithOptions(x => x.GenericRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder<TContainer> UseRegistrationConstraintBehavior<TContainer>(this ShepherdBuilder<TContainer> target,
			IRegistrationConstraintBehavior behavior)
		{
			target.WithOptions(x => x.RegistrationConstraintBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder<TContainer> UseSingleServiceRegistrationBehavior<TContainer>(this ShepherdBuilder<TContainer> target,
			ISingleServiceRegistrationBehavior<TContainer> behavior)
		{
			target.WithOptions(x => x.SingleServiceRegistrationBehavior = behavior);

			return target;
		}
	}

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
	}
}
