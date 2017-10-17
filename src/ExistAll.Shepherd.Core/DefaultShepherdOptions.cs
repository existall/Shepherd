using System;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public class ShepherdOptions : IShepherdOptions
	{
		internal Action<ContainerOptions> ContainerConfiguration { get; set; }

		public void ConfigureContainerOptions(Action<ContainerOptions> options)
		{
			ContainerConfiguration = options;	
		}

		public ITypeMatcher TypeMatcher { get; set; }
	}
}