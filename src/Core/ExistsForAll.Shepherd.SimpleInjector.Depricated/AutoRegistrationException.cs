using System;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal class AutoRegistrationException : Exception
	{
		public AutoRegistrationException(string message) : base(message)
		{
		}

		public AutoRegistrationException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}