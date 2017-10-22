using System.Collections.Generic;
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
		private readonly IEnumerable<IRegistrationActionCandidate> _actions;
		public GenericRegistration(IShepherdOptions shepherdOptions)
		{
			Options = shepherdOptions;
			_actions = ArrangeRegistrationOrder(Options);
		}

		private static IEnumerable<IRegistrationActionCandidate> ArrangeRegistrationOrder(IShepherdOptions options)
		{
			var actions = new List<IRegistrationActionCandidate>
			{
				options.DecoratorRegistration,
				options.CollectionRegistration,
				options.SingleServiceRegistration
			};

			return actions;
		}

		public virtual bool ShouldRegister(IServiceDescriptor descriptor)
		{
			return descriptor.ServiceType.GetTypeInfo().IsGenericType || descriptor.ServiceType.GetTypeInfo().ContainsGenericParameters;
		}

		public virtual void Register(IRegistrationContext context, Container container)
		{
			var candidateDescriptor = context.AsRegistrationContext();

			foreach (var action in _actions)
			{
				if (!action.ShouldRegister(candidateDescriptor))
					continue;

				action.Register(context, container);
				return;
			}
		}
	}
}