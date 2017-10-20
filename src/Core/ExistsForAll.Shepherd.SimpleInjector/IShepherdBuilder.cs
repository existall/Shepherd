namespace ExistsForAll.Shepherd.SimpleInjector
{
	public interface IShepherdBuilder
	{
		IShepherdOptions Options { get; }
		IShepherd Build();
	}
}