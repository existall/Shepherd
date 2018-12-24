using System;
using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core.Extensions
{
	internal static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> target, Action<T> action)
		{
			if (target == null)
				return;

			foreach (var item in target) action(item);
		}
	}
}
