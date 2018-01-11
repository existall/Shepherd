﻿using System;
using System.Collections.Generic;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;

namespace ExistsForAll.Shepherd.SimpleInjector.Filters
{
	public class InterfaceAccumulationFilter : IInterfaceAccumulationFilter
	{
		public HashSet<Type> Interfaces { get; } = new HashSet<Type>();

		public InterfaceAccumulationFilter(params Type[] typesToFilter)
		{
			typesToFilter.ForEach(x => Interfaces.Add(x));
		}

		public bool ShouldExcludeInterface(Type @interface)
		{
			return Interfaces.Contains(@interface);
		}
	}
}