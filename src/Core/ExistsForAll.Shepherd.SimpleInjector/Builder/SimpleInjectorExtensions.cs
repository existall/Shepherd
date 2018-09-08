using System.Collections.Generic;
using System.Text;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Builder
{
	public static class SimpleInjectorExtensions
	{
		public static ShepherdBuilder UseShepherd(this Container container)
		{
			return new ShepherdBuilder(container);
		}
	}
}
