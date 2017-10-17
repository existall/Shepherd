using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ExistAll.Shepherd.Core
{
	//public abstract class Shepherd<T>
	//{
	//	private readonly T _container;

	//	//public IServiceRegistrator ServiceRegistrator { get; set; } = new WindsorServiceRegistrator();

	//	protected Shepherd(T container)
	//	{
	//		_container = container;
	//	}

	//	public Options Options { get; }

	//	public IServiceResolver Initialize(AssemblyCollection assemblyCollection, ModuleCollection modules)
	//	{
	//		var types = assemblyCollection.GetAllTypes().ToArray();
	//		var assemblies = assemblyCollection.Assemblies.ToArray();

	//		IInitializingContext<T> context = CreateInitializingContext(_container, types, assemblies);

	//		modules.ForEach(x =>
	//		{
	//			//x.Run(context);
	//		});

	//		return null;
	//	}

	//	protected abstract IInitializingContext<T> CreateInitializingContext(T container, Type[] types, Assembly[] assemblies);
	//}

	public interface IInitializingContext
	{
		IEnumerable<Assembly> Assemblies { get; }
		IEnumerable<Type> Types { get; }
	}

	public interface IInitializingContext<out T> : IInitializingContext
	{
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

	public interface IModuleContext
	{
		IEnumerable<Assembly> Assemblies { get; }
		IEnumerable<Type> Types { get; }
		Container Container { get; }
	}

	internal class ModuleContext : IModuleContext
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }
		public Container Container { get; }

		protected ModuleContext(IEnumerable<Assembly> assemblies, IEnumerable<Type> types, Container container)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
		}
	}
}