using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated.Filters
{
	public interface IInterfaceAccumulationFilter : IFilter
	{
		bool ShouldExcludeInterface(Type @interface);
	}
}