namespace ExistsForAll.Shepherd.Core
{
	internal interface IOptionsValidator<TContainer>
	{
		void ValidateOptions(IShepherdOptions<TContainer> options);
	}
}