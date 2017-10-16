using System;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IShepherdOptions
	{
		void ConfigureContainerOptions(Action<ContainerOptions> options);
		ITypeMatcher TypeMatcher { get; set; }
	}
}