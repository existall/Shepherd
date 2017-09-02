namespace ExistAll.Shepherd.Core
{
	public interface IShepherdOptions
	{
		object Container { get; }
	}

	public interface IShepherdOptions<out T> : IShepherdOptions
	{
		T Container { get; }
	}
}