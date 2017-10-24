namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests.Subjects
{
	public class OpenGenerics<T> : IOpenGenerics<T>
	{
		public T TypeOfGeneric { get; set; }
	}
}