﻿using System;
using System.Collections.Generic;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Filters
{
	public class ImplementationAccumulationFilter : IImplementationAccumulationFilter
	{
		private HashSet<Type> Implementations { get; } = new HashSet<Type>();

		public ImplementationAccumulationFilter(params Type[] implementations)
		{
			implementations.ForEach(x => Implementations.Add(x));
		}

		public bool ShouldExcludeClass(Type implementation)
		{
			return Implementations.Contains(implementation);
		}
	}
}