using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
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
			return descriptor.ServiceType.GetTypeInfo().IsGenericType || descriptor.ServiceType.GetTypeInfo().ContainsGenericParameters;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			var supportedTypes = container.GetTypesToRegister(context.ServiceType, context.Assemblies,
					new TypesToRegisterOptions { IncludeGenericTypeDefinitions = true })
				.ToArray();

			var actions = new List<IRegistrationActionCandidate>()
			{
				Options.DecoratorRegistration,
				Options.CollectionRegistration,
				Options.SingleServiceRegistration
			};

			var candidateDescriptor = new CandidateDescriptor(context.ServiceType, supportedTypes);

			foreach (var action in actions)
			{
				if (action.ShouldRegister(candidateDescriptor))
				{
					action.Register(new RegistrationContext(context.ServiceType, supportedTypes, context.Assemblies), container);
					return;
				}
			}


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