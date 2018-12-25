using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal interface IModulesExecutor<TContainer>
	{
		void ExecuteModules(ModuleCollection<TContainer> modules,
			TContainer container,
			IEnumerable<Assembly> assemblies,
			IEnumerable<Type> types, 
			IShepherdOptions<TContainer> options);
	}
}