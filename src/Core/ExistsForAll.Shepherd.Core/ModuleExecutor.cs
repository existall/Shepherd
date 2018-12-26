using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core
{
	internal class ModuleExecutor<TContainer> : IModulesExecutor<TContainer>
	{
		public void ExecuteModules(ModuleCollection<TContainer> modules, TContainer container, IEnumerable<Assembly> assemblies, IEnumerable<Type> types,
			IShepherdOptions<TContainer> options)
		{
			var context = new ModuleContext<TContainer>(assemblies, types, container, options);

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