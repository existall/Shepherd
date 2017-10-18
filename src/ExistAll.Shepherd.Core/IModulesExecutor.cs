using System;
using System.Reflection;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	internal interface IModulesExecutor
	{
		void ExecuteModules(ModuleCollection modules, Container container, Assembly[] assemblies, Type[] types);
	}
}