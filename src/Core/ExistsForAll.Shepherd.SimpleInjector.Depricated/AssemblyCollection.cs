using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public class AssemblyCollection
	{
		private readonly List<IAssemblyLoader> _list = new List<IAssemblyLoader>();
		private readonly List<Assembly> _assemblies = new List<Assembly>();
		private readonly Lazy<Type[]> _allTypes;

		public AssemblyCollection()
		{
			_allTypes = new Lazy<Type[]>(GetAllTypesImp);
		}

		internal IEnumerable<Assembly> Assemblies => _assemblies;

		public void Add(IAssemblyLoader assemblyLoader)
		{
			if (assemblyLoader == null) throw new ArgumentNullException(nameof(assemblyLoader));

			ActionConstraint(() =>
			{
				_list.Add(assemblyLoader);
				_assemblies.Add(assemblyLoader.Assembly);
			});
		}

		public void AddRange(params IAssemblyLoader[] loaders)
		{
			if (loaders == null) throw new ArgumentNullException(nameof(loaders));

			ActionConstraint(() =>
			{
				_list.AddRange(loaders);
				_assemblies.AddRange(loaders.Select(x => x.Assembly));
			});
		}

		internal IEnumerable<Type> GetAllTypes()
		{
			return _allTypes.Value;
		}

		private Type[] GetAllTypesImp()
		{
			return _list.SelectMany(x => x.Types()).ToArray();
		}

		private void ActionConstraint(Action action)
		{
			if (action == null) throw new ArgumentNullException(nameof(action));

			if (_allTypes.IsValueCreated)
				throw new InvalidOperationException("Adding assemblies after GetAllTypes is forbidding");

			action.Invoke();
		}
	}
}