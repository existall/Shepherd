using System;

namespace ExistAll.Shepherd.Core
{
	public class ShepherdBuilder : IShepherdBuilder
	{
		private IShepherdOptions _options;
		private Type _contianerType;
		private object _container;


		public IShepherdBuilder AddOptions<T>(IShepherdOptions<T> shepherdOptions)
		{
			_contianerType = typeof(T);
			_options = shepherdOptions;
			_container = shepherdOptions.Container;
			return this;
		}

		public IShepherdOptions Options { get; } = new 

		public IShepherd Build()
		{
			return new Shepherd(_options);
		}
	}
}