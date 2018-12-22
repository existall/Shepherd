using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	public interface IRegistrationContext<out TContainer>
	{
		TContainer Container { get; }
		Type ServiceType { get; }
		IEnumerable<Type> ImplementationTypes { get; }
		IEnumerable<Assembly> Assemblies { get; }
	}
}