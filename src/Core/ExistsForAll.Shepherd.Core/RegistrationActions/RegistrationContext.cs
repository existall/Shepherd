using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	internal struct RegistrationContext<TContainer> : IRegistrationContext<TContainer>
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }
		public IEnumerable<Assembly> Assemblies { get; }
		
		public Dictionary<string, object> Properties { get; }
		public TContainer Container { get; }

		public RegistrationContext(Type serviceType,
			IEnumerable<Type> implementationTypes,
			IEnumerable<Assembly> assemblies, TContainer container)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
			Assemblies = assemblies;
			Container = container;
			Properties = new Dictionary<string, object>();
		}
	}
}