using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistsForAll.Shepherd.SimpleInjector.Extensions;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.RegistrationActions
{
	public interface IGenericRegistration : IRegistrationActionCandidate
	{ }

	public class GenericRegistration : IGenericRegistration
	{
		private IShepherdOptions Options { get; }
		private IRegistrationActionCandidate[] _actions;
		public GenericRegistration(IShepherdOptions shepherdOptions)
		{
			Options = shepherdOptions;

			_actions = ArrangeRegistrationOrder(Options);
		}

		private IRegistrationActionCandidate[] ArrangeRegistrationOrder(IShepherdOptions options)
		{
			var actions = new List<IRegistrationActionCandidate>()
			{
				Options.DecoratorRegistration,
				Options.CollectionRegistration,
				Options.SingleServiceRegistration
			};
		}

		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return descriptor.ServiceType.GetTypeInfo().IsGenericType || descriptor.ServiceType.GetTypeInfo().ContainsGenericParameters;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			var supportedTypes = container.GetTypesToRegister(
				context.ServiceType,
				context.Assemblies,
				new TypesToRegisterOptions { IncludeGenericTypeDefinitions = true })
				.ToArray();

			var candidateDescriptor = context.AsRegistrationContext();

			foreach (var action in _actions)
			{
				if (action.ShouldRegister(candidateDescriptor))
				{
					action.Register(new RegistrationContext(context.ServiceType, supportedTypes, context.Assemblies), container);
					return;
				}
			}
		}
	}
}