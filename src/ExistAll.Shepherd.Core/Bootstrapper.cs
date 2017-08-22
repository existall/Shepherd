using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ExistAll.Shepherd.Core
{
	public abstract class Shepherd<T>
	{
		private readonly T _container;

		//public IServiceRegistrator ServiceRegistrator { get; set; } = new WindsorServiceRegistrator();

		protected Shepherd(T container)
		{
			_container = container;
		}

		public Options Options { get; }

		public IServiceResolver Initialize(AssemblyCollection assemblyCollection, ModuleCollection modules)
		{
			var types = assemblyCollection.GetAllTypes().ToArray();
			var assemblies = assemblyCollection.Assemblies.ToArray();
			
			modules.ForEach(x =>
			{
				x.Run(IInitializingContext );
			});

			return null;
		}
	}

	public interface IInitializingContext<T>
	{
		IEnumerable<Assembly> Assemblies { get; }
		IEnumerable<Type> Types { get; }
		T Container { get; }
	}

	public abstract class InitializingContext<T> : IInitializingContext<T>
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }

		public T Container { get; }

		protected InitializingContext(IEnumerable<Assembly> assemblies, IEnumerable<Type> types, T container)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
		}
	}
}