using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Filters
{
	public interface IImplementationAccumulationFilter : IFilter
	{
		bool ShouldExcludeClass(Type implementation);
	}
}