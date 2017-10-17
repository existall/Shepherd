using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using ExistAll.Shepherd.Core.Extensions;

namespace ExistAll.Shepherd.Core
{
	//public class ShepherdBuilder : IShepherdBuilder
	//{
	//	private IShepherdOptions _options;
	//	private Type _contianerType;
	//	private object _container;


	//	public IShepherdBuilder AddOptions<T>(IShepherdOptions<T> shepherdOptions)
	//	{
	//		_contianerType = typeof(T);
	//		_options = shepherdOptions;
	//		_container = shepherdOptions.Container;
	//		return this;
	//	}

	//	public IShepherdOptions Options { get; }

	//	public IShepherd Build()
	//	{
	//		return new Shepherd(_options);
	//	}
	//}

	public interface ITypeMatcher
	{
		IEnumerable<Type> FilterTypes(IEnumerable<Type> applicationTypes);
		IEnumerable<KeyValuePair<Type, IEnumerable<Type>>> MapTypes(IEnumerable<Type> applicationTypes);
	}


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