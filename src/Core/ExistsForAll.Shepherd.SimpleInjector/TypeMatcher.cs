using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class TypeMatcher : ITypeMatcher
	{
		public Predicate<Type> TypeFilter { get; set; } = type => true;

		public IEnumerable<ServiceTypeMap> MapTypes(IEnumerable<Type> applicationTypes)
		{
			if(TypeFilter == null)
				throw new AutoRegistrationException(ExceptionMessages.MissingTypeFilterMessage);

			var types = applicationTypes.Where(x => TypeFilter(x)).ToArray();

			var mapper = new Dictionary<Type, List<Type>>();

			types.Where(x => x.GetTypeInfo().IsInterface)
				.ForEach(x => mapper.Add(x, new List<Type>()));

			types.Where(x => !x.GetTypeInfo().IsInterface && !x.GetTypeInfo().IsAbstract)
				.ForEach(typeCandidate =>
				{
					foreach (var @interface in typeCandidate.GetTypeInfo().GetInterfaces())
					{
						if (mapper.ContainsKey(@interface))
							mapper[@interface].Add(typeCandidate);
					}
				});

			return  mapper.Select(x => new ServiceTypeMap(x.Key,x.Value))
				.ToArray();
		}
	}
}