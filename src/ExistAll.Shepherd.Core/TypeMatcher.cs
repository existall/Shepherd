using System;
using System.Collections.Generic;
using System.Linq;
using ExistAll.Shepherd.Core.Extensions;

namespace ExistAll.Shepherd.Core
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

			types.Where(x => x.IsInterface)
				.ForEach(x => mapper.Add(x, new List<Type>()));

			types.Where(x => !x.IsInterface && !x.IsAbstract)
				.ForEach(typeCandidate =>
				{
					foreach (var @interface in typeCandidate.GetInterfaces())
					{
						if (mapper.ContainsKey(@interface))
							mapper[@interface].Add(typeCandidate);
					}
				});

			return (IEnumerable<KeyValuePair<Type, IEnumerable<Type>>>) mapper;
		}
	}
}