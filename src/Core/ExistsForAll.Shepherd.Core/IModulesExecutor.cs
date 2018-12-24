using System;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal interface IModulesExecutor<TContainer>
	{
		void ExecuteModules(ModuleCollection<TContainer> modules, TContainer container, Assembly[] assemblies, Type[] types);
	}
}