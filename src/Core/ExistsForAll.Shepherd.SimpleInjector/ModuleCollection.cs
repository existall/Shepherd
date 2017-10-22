using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class ModuleCollection : List<IModule>
	{
		public void AddModule(IModule module, params IModule[] modules)
		{
			Add(module);

			if (modules != null)
				AddRange(modules);
		}
	}
}