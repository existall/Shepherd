using System;
using System.Reflection;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	internal class ModuleExecutor : IModulesExecutor
	{
		public void ExecuteModules(ModuleCollection modules, Container container, Assembly[] assemblies, Type[] types)
		{
			var context = new ModuleContext(assemblies, types, container);

			foreach (var module in modules)
			{
				module.Configure(context);
			}

		}
	}
}