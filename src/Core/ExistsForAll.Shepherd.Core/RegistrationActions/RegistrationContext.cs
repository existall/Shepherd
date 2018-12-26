using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ExistsForAll.Shepherd.Core.RegistrationActions
{
	internal struct RegistrationContext<TContainer> : IRegistrationContext<TContainer>
	{
		public Type ServiceType { get; }
		public IEnumerable<Type> ImplementationTypes { get; }
		public IEnumerable<Assembly> Assemblies { get; }
		
		public IReadOnlyDictionary<string, object> Properties { get; }
		public TContainer Container { get; }

		public RegistrationContext(Type serviceType,
			IEnumerable<Type> implementationTypes,
			IEnumerable<Assembly> assemblies,
			TContainer container,
			IShepherdOptions<TContainer> options)
		{
			ServiceType = serviceType;
			ImplementationTypes = implementationTypes;
			Assemblies = assemblies;
			Container = container;
			Properties = new ReadOnlyDictionary<string, object>(options.Items);
		}
	}
}