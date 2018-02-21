using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects
{
	public class SingleTypeCollectionHolder : ISingleTypeCollectionHolder
	{
		public IEnumerable<ISingleTypeCollection> Collection { get; }

		public ISingleTypeCollection[] SingleTypeCollections { get; }
		public SingleTypeCollectionHolder(IEnumerable<ISingleTypeCollection> singleTypeCollections)
		{
			//Collection = collection;
			Collection = singleTypeCollections;
		}
	}
}