using System;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal class ModuleExecutor<TContainer> : IModulesExecutor<TContainer>
	{
		public void ExecuteModules(ModuleCollection<TContainer> modules, TContainer container, Assembly[] assemblies, Type[] types)
		{
			var context = new ModuleContext<TContainer>(assemblies, types, container);

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