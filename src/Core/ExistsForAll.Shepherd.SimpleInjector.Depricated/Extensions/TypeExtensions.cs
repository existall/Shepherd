using System;
using System.Linq;
using System.Reflection;


namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions
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

		public static bool IsInterface(this Type type) => type.Info().IsInterface;

		public static TypeInfo Info(this Type type) => type.GetTypeInfo();

		public static Type[] GetGenericArguments(this Type type) => type.Info().GetGenericArguments();

	    public static string ToFriendlyName(this Type type)
	    {
	        return TypeNameHelper.GetTypeDisplayName(type, includeGenericParameterNames: true);
	    }

	    public static bool IsAssignableTo(this Type type, Type otherType)
	    {
	        var typeInfo = type.GetTypeInfo();
	        var otherTypeInfo = otherType.GetTypeInfo();

	        if (otherTypeInfo.IsGenericTypeDefinition)
	        {
	            return typeInfo.IsAssignableToGenericTypeDefinition(otherTypeInfo);
	        }

	        return otherTypeInfo.IsAssignableFrom(typeInfo);
	    }

	    private static bool IsAssignableToGenericTypeDefinition(this TypeInfo typeInfo, TypeInfo genericTypeInfo)
	    {
	        var interfaceTypes = typeInfo.ImplementedInterfaces.Select(t => t.GetTypeInfo());

	        foreach (var interfaceType in interfaceTypes)
	        {
		        if (!interfaceType.IsGenericType)
			        continue;
		        
		        var typeDefinitionTypeInfo = interfaceType
			        .GetGenericTypeDefinition()
			        .GetTypeInfo();

		        if (typeDefinitionTypeInfo.Equals(genericTypeInfo))
		        {
			        return true;
		        }
	        }

	        if (typeInfo.IsGenericType)
	        {
	            var typeDefinitionTypeInfo = typeInfo
	                .GetGenericTypeDefinition()
	                .GetTypeInfo();

	            if (typeDefinitionTypeInfo.Equals(genericTypeInfo))
	            {
	                return true;
	            }
	        }

	        var baseTypeInfo = typeInfo.BaseType?.GetTypeInfo();

	        if (baseTypeInfo == null)
	        {
	            return false;
	        }

	        return baseTypeInfo.IsAssignableToGenericTypeDefinition(genericTypeInfo);
	    }
	}
}
