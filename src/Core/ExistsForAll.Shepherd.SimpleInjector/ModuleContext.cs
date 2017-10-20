using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
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

	internal class ModuleContext : IModuleContext
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }
		public Container Container { get; }

		public ModuleContext(IEnumerable<Assembly> assemblies,
			IEnumerable<Type> types,
			Container container)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
		}
	}
}