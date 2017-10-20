using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	public class TypeMatcher : ITypeMatcher
	{
		public IEnumerable<Type> FilterTypes(IEnumerable<Type> applicationTypes)
		{
			return applicationTypes;
		}

		public IEnumerable<KeyValuePair<Type, IEnumerable<Type>>> MapTypes(IEnumerable<Type> applicationTypes)
		{
			var types = applicationTypes.ToArray();

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

			return (IEnumerable<KeyValuePair<Type, IEnumerable<Type>>>) mapper;
		}
	}
}