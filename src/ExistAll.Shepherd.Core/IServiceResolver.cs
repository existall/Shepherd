using System;
using System.Collections;
using System.Collections.Generic;

namespace ExistAll.Shepherd.Core
{
	public interface IServiceResolver : IDisposable
	{
		object Resolve(Type service);

		T Resolve<T>() where T : class;

		IEnumerable<T> ResolveAll<T>() where T : class;

		IEnumerable ResolveAll(Type type);
	}

	public interface IServiceResolver<out TContainer> : IServiceResolver
	{
		TContainer Container { get; }
	}
}