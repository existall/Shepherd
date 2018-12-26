namespace ExistsForAll.Shepherd.DependencyInjection.UnitTests
{
    public partial class ServiceIndexerTests
	{
		public interface IInterface
		{

		}

	    public class Interface : IInterface
		{
		}

	    public interface IInterface2
		{

		}

	    public class Interface2 : IInterface2
		{
		}

	    public interface IOpenGenericsInterface<T>
		{

		}

	    public class OpenGenericsInterface<T> : IOpenGenericsInterface<T>
		{
		}

	    public interface IOpenCloseGenericsInterface<T>
		{

		}

	    public class IntOpenCloseGenericsInterface : IOpenCloseGenericsInterface<int>
		{
		}

	    public class StringOpenCloseGenericsInterface : IOpenCloseGenericsInterface<string>
		{
		}
	}
}
