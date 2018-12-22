using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core
{
	public class ModuleCollection<TContainer> : List<IModule<TContainer>>
	{
		public void AddModule(IModule<TContainer> module, params IModule<TContainer>[] modules)
		{
			Add(module);

			if (modules != null)
				AddRange(modules);
		}
	}
}