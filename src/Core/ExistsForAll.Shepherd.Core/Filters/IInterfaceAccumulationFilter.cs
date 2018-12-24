using System;

namespace ExistsForAll.Shepherd.Core.Filters
{
	public interface IInterfaceAccumulationFilter : IFilter
	{
		bool ShouldExcludeInterface(Type @interface);
	}
}