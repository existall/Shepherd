using System;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	internal interface IModulesExecutor
	{
		void ExecuteModules(ModuleCollection modules, Container container, Assembly[] assemblies, Type[] types);
	}
}