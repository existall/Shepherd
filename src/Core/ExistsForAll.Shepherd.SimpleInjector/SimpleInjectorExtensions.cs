using ExistsForAll.Shepherd.SimpleInjector.Builder;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public static class SimpleInjectorExtensions
	{
		public static ShepherdBuilder UseShepherd(this Container container)
		{
			return new ShepherdBuilder(container);
		}
	}
}
