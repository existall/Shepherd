namespace ExistAll.Shepherd.Core
{
	public interface IShepherdBuilder
	{
		IShepherdOptions Options { get; }
		IShepherd Build();
	}
}