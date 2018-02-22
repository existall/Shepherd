using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects
{
	public interface ISingleTypeCollectionHolder
	{
		IEnumerable<ISingleTypeCollection> Collection { get; }
	}
}