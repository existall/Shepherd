using System;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	internal static class TypeExtensions
	{
		public static bool IsGeneric(this Type target)
		{
			var info = target.GetTypeInfo();
			return info.IsGenericType || info.IsGenericTypeDefinition;
		}
	}
}
