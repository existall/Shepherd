using System;

namespace ExistAll.Shepherd.Core
{
	public interface IShepherdOptions
	{
		IContainerOptionsConfiguration ConfigureContainerOptions { get; set; }
		ITypeMatcher TypeMatcher { get; set; }
	}
}