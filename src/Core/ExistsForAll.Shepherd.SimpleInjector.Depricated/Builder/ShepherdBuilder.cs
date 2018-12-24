using System;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Filters;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.RegistrationActions;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Builder
{
	public static class ShepherdBuilderExtensions
	{
		public static ShepherdBuilder WithAssembly<T>(this ShepherdBuilder target)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			target.WithAssemblies(x => x.AddAssemblies(assembly));

			return target;
		}

		public static ShepherdBuilder WithExportedAssembly<T>(this ShepherdBuilder target)
		{
			var assembly = typeof(T).GetTypeInfo().Assembly;

			target.WithAssemblies(x => x.AddExportedAssemblies(assembly));

			return target;
		}

		public static ShepherdBuilder UseServiceIndexer(this ShepherdBuilder target, IServiceIndexer serviceIndexer)
		{
			target.WithOptions(x => x.ServiceIndexer = serviceIndexer);

			return target;
		}

		public static ShepherdBuilder UseServiceIndexerFilter(this ShepherdBuilder target, IFilter filter)
		{
			target.WithOptions(x => x.ServiceIndexer.Filters.Add(filter));

			return target;
		}

		public static ShepherdBuilder UseCollectionRegistrationBehavior(this ShepherdBuilder target,
			ICollectionRegistrationBehavior behavior)
		{
			target.WithOptions(x => x.CollectionRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder UseDecoratorRegistrationBehavior(this ShepherdBuilder target,
			IDecoratorRegistrationBehavior behavior)
		{
			target.WithOptions(x => x.DecoratorRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder UseGenericRegistrationBehavior(this ShepherdBuilder target,
			IGenericRegistrationBehavior behavior)
		{
			target.WithOptions(x => x.GenericRegistrationBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder UseRegistrationConstraintBehavior(this ShepherdBuilder target,
			IRegistrationConstraintBehavior behavior)
		{
			target.WithOptions(x => x.RegistrationConstraintBehavior = behavior);

			return target;
		}

		public static ShepherdBuilder UseSingleServiceRegistrationBehavior(this ShepherdBuilder target,
			ISingleServiceRegistrationBehavior behavior)
		{
			target.WithOptions(x => x.SingleServiceRegistrationBehavior = behavior);

			return target;
		}
	}

	public class ShepherdBuilder
	{
		private Shepherd Shepherd { get; }

		public ShepherdBuilder(Shepherd shepherd)
		{
			Shepherd = shepherd;
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
	}
}
