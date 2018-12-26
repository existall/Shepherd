using System;
using System.Collections.Generic;
using System.Linq;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Filters;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Resources;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Extensions;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	public class ServiceIndexer : IServiceIndexer
	{
		public FilterCollection Filters { get; } = new FilterCollection();

		public virtual IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes)
		{
			if (Filters == null)
				throw new AutoRegistrationException(ExceptionMessages.MissingTypeFilterMessage);

			var allTypes = applicationTypes.ToArray();

			var serviceMaps = allTypes
				.Where(x => x.IsInterface())
				.Where(x => Filters.OfType<IInterfaceAccumulationFilter>()
					.All(filter => !filter.ShouldExcludeInterface(x)))
				.Select(interfaceType =>

					new ServiceTypeMap(interfaceType,
						allTypes.Where(x => !x.IsAbstract() && x.IsClass() && x.IsAssignableTo(interfaceType))
							.Where(x => Filters.OfType<IImplementationAccumulationFilter>()
								.All(filter => !filter.ShouldExcludeClass(x)))
							.ToArray()))
				.ToArray();

			return serviceMaps;
		}
	}
}