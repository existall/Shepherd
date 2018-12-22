using System;

namespace ExistsForAll.Shepherd.Core.Filters
{
	public interface IImplementationAccumulationFilter : IFilter
	{
		bool ShouldExcludeClass(Type implementation);
	}
}