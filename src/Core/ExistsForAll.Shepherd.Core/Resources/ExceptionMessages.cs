﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ExistsForAll.Shepherd.Core.Resources
{
	public static class ExceptionMessages
	{
		public const string SkipRegistrationMessage = @"RegistrationConstraintBehavior Attribute was not set for RegistrationConstraintBehavior in ShepheredOptions.
An attribute type must be set in order for the current configuration.
Another functionality can be provided by overloading Register method or replacing IRegistrationConstraintBehavior interface";

		public const string MissingTypeFilterMessage = @"While gathering all service types and implemintations in TypeMatcher.MapTypes method
 the ExclusionTypeFilter predicat is null. Please provide one";

		public static string ModuleExecutionExceptionMessage(string moduleName) => $@"While executing module {moduleName} an exception has occurred.";

		public static string DecoratorRegistrationExceptionMessage(Type serviceType, IEnumerable<Type> implTypes) => $@"Shepherd checked if service {serviceType.Name} uses Decorator.
While one of the implementation is a decorator more than {implTypes.Count()} implementations found. To be able to use decorator please use two implementations or a Shepherd Module for custom registration.";

		public static string AutoRegisterArrayExceptionMessage(Type type)
		{
			return $@"Shepherd tried to auto register [{type.FullName}] as array, arrays are not supported as dependecies it should be requested as IEnumerable<>";
		}

		public static string AutoRegisterCollectionExceptionMessage(Type type)
		{
			return $@"Shepherd tried to Auto Register {type.FullName} as a collection, yet no implementation was found.";
		}
	}
}