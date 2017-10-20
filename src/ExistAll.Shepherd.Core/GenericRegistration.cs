﻿using System.Linq;
using SimpleInjector;

namespace ExistAll.Shepherd.Core
{
	public interface IGenericRegistration : IRegistrationActionCandidate
	{ }

	public class GenericRegistration : IGenericRegistration
	{
		private IShepherdOptions Options { get; }

		public GenericRegistration(IShepherdOptions shepherdOptions)
		{
			Options = shepherdOptions;
		}

		public virtual bool ShouldRegister(ICandidateDescriptor descriptor)
		{
			return descriptor.ServiceType.IsGenericType || descriptor.ServiceType.ContainsGenericParameters;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			var supportedTypes = container.GetTypesToRegister(context.ServiceType, context.Assemblies,
					new TypesToRegisterOptions { IncludeGenericTypeDefinitions = true})
				.ToArray();

			//if(Options.DecoratorRegistration

			//if (reg.Interface.HasAttribute<RegisterAllImplementationsAttribute>())
			//{
			//	container.RegisterCollection(reg.Interface, supportedtypes.Select(x => Lifestyle.Singleton.CreateRegistration(x, container)));
			//}

			//foreach (var imp in supportedtypes)
			//{
			//	container.RegisterSingleton(reg.Interface, imp);
			//}			//if(Options.DecoratorRegistration

			//if (reg.Interface.HasAttribute<RegisterAllImplementationsAttribute>())
			//{
			//	container.RegisterCollection(reg.Interface, supportedtypes.Select(x => Lifestyle.Singleton.CreateRegistration(x, container)));
			//}

			//foreach (var imp in supportedtypes)
			//{
			//	container.RegisterSingleton(reg.Interface, imp);
			//}
		}
	}
}