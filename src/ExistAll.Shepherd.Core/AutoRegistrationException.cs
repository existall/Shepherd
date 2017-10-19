using System;
using System.Runtime.Serialization;

namespace ExistAll.Shepherd.Core
{
	[Serializable]
	internal class AutoRegistrationException : Exception
	{
		public AutoRegistrationException(string message) : base(message)
		{
		}

		public AutoRegistrationException(string message, Exception inner) : base(message, inner)
		{
		}

		protected AutoRegistrationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}