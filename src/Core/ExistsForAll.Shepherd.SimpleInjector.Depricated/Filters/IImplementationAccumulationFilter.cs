using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Filters
{
	public interface IImplementationAccumulationFilter : IFilter
	{
		bool ShouldExcludeClass(Type implementation);
	}
}