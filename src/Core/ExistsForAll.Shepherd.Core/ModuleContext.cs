using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal class ModuleContext<TContainer> : IModuleContext<TContainer>
	{
		public IEnumerable<Assembly> Assemblies { get; }
		public IEnumerable<Type> Types { get; }
		public TContainer Container { get; }
		
		public IReadOnlyDictionary<string,object> Properties { get; }

		public ModuleContext(IEnumerable<Assembly> assemblies,
			IEnumerable<Type> types,
			TContainer container,
			IShepherdOptions<TContainer> options)
		{
			Assemblies = assemblies;
			Types = types;
			Container = container;
			Properties = new ReadOnlyDictionary<string, object>(options.Items);
		}
	}
}