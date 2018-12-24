using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal static class Guard
	{
		public static void NullArgument<T>(T obj, string name) where T : class
		{
			if (obj == null)
				throw new ArgumentNullException(name);
		}
	}
}
