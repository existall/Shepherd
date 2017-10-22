using System;
using System.Reflection;

namespace ExistsForAll.Shepherd.SimpleInjector.Extensions
{
	internal static class TypeExtensions
	{
		public static bool IsGeneric(this Type target) => target.IsGenericType() || target.IsGenericTypeDefinition();
		public static bool IsGenericType(this Type type) => type.Info().IsGenericType;
		public static bool IsGenericTypeDefinition(this Type type) => type.Info().IsGenericType;
		public static Type GetGenericTypeDefinition(this Type type) => type.Info().GetGenericTypeDefinition();
		public static bool IsClass(this Type type) => type.Info().IsClass;
		public static bool IsAbstract(this Type type) => type.Info().IsAbstract;

		public static bool IsAssignableFrom(this Type type, Type assignableType) => type.Info().IsAssignableFrom(assignableType);
		public static TypeInfo Info(this Type type) => type.GetTypeInfo();
	}
}
