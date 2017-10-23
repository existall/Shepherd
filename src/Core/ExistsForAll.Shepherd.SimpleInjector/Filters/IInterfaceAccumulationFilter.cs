using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Filters
{
	public interface IInterfaceAccumulationFilter : IFilter
	{
		bool ShouldExcludeInterface(Type @interface);
	}
}