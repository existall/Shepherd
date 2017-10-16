namespace ExistAll.Shepherd.Core
{
	internal interface IOptionsValidator
	{
		void ValidateOptions(IShepherdOptions options);
	}

	class OptionsValidator : IOptionsValidator
	{
		public void ValidateOptions(IShepherdOptions options)
		{
			
		}
	}
}