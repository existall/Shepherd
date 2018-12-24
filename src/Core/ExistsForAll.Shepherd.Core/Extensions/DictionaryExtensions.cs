using System.Collections.Generic;

namespace ExistsForAll.Shepherd.Core.Extensions
{
	public static class DictionaryExtensions
	{
		public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> target, TKey key, TValue value)
		{
			if (target.ContainsKey(key))
				target[key] = value;
			else
				target.Add(key, value);
		}
		
	}
}