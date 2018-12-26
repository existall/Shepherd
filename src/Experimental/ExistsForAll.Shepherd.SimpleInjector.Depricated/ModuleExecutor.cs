using System;
using System.Reflection;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal class ModuleExecutor : IModulesExecutor
	{
		public void ExecuteModules(ModuleCollection modules, Container container, Assembly[] assemblies, Type[] types)
		{
			var context = new ModuleContext(assemblies, types, container);

			foreach (var module in modules)
			{
				try
				{
					module.Configure(context);
				}
				catch (Exception e)
				{
					throw new ModuleExecutionException(module.GetType().FullName, e);
				}
			}
		}
	}
}