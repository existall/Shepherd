using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class ServiceIndexer : IServiceIndexer
	{
		public Predicate<Type> TypeFilter { get; set; } = type => true;

		public IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes)
		{
			if (TypeFilter == null)
				throw new AutoRegistrationException(ExceptionMessages.MissingTypeFilterMessage);

			var types = applicationTypes.Where(x => TypeFilter(x)).ToArray();

			var mapper = new Dictionary<Type, List<Type>>();
			var genericMapper = new Dictionary<Type, List<Type>>();

			types.Where(x => x.GetTypeInfo().IsInterface)
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

			types.Where(x => x.IsClass() && !x.IsAbstract())
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
				.Select(x => new ServiceTypeMap(x.Key, x.Value))
				.ToArray();
		}
	}
}