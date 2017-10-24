using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Filters;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class ServiceIndexer : IServiceIndexer
	{
		public FilterCollection Filters { get; } = new FilterCollection();

		public virtual IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes)
		{
			if (Filters == null)
				throw new AutoRegistrationException(ExceptionMessages.MissingTypeFilterMessage);

			var allTypes = applicationTypes.ToArray();

			var mapper = new Dictionary<Type, List<Type>>();
			var genericMapper = new Dictionary<Type, List<Type>>();

			allTypes.Where(x => x.IsInterface())
				.Where(x => Filters.OfType<IInterfaceAccumulationFilter>()
				.All(filter => filter.ShouldExcludeInterface(x)))
				.ForEach(@interface =>
				{
					if (@interface.IsGenericType())
					{
						genericMapper.Add(@interface, new List<Type>());
					}
					else
					{
						mapper.Add(@interface, new List<Type>());
					}
				});

			allTypes.Where(x => x.IsClass() && !x.IsAbstract())
				.Where(x => Filters.OfType<IImplementationAccumulationFilter>()
				.All(filter => filter.ShouldExcludeClass(x)))
				.ForEach(typeCandidate =>
				{
					foreach (var @interface in typeCandidate.GetTypeInfo().GetInterfaces())
					{
						if (@interface.IsGenericType())
						{
							var @class = genericMapper.First(x => x.Key.GetGenericTypeDefinition() == @interface.GetGenericTypeDefinition());

							@class.Value.Add(typeCandidate);
							continue;
						}

						if (mapper.ContainsKey(@interface))
							mapper[@interface].Add(typeCandidate);
					}
				});

			return genericMapper.Concat(mapper)
				.Where(x => x.Value != null)
				.Select(x => new ServiceTypeMap(x.Key, x.Value))
				.ToArray();
		}
	}
}