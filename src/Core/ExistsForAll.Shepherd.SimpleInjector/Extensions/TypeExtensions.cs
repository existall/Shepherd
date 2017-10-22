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

		public static bool IsGenericType(this Type type) => type.Info().IsGenericType;
		public static bool IsGenericTypeDefinition(this Type type) => type.Info().IsGenericType;
		public static Type GetGenericTypeDefinition(this Type type) => type.Info().GetGenericTypeDefinition();

		public static TypeInfo Info(this Type type) => type.GetTypeInfo();
	}
}
