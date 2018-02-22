using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects
{
	public class SingleTypeCollectionHolder : ISingleTypeCollectionHolder
	{
		public IEnumerable<ISingleTypeCollection> Collection { get; }

		public SingleTypeCollectionHolder(IEnumerable<ISingleTypeCollection> singleTypeCollections)
		{
			Collection = singleTypeCollections;
		}
	}
}