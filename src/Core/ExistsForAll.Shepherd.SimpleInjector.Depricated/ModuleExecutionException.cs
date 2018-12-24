using System;
using ExistsForAll.Shepherd.SimpleInjector.Depricated.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector.Depricated
{
	internal class ModuleExecutionException : Exception
	{
		public ModuleExecutionException(string moduleName, Exception e)
			: base(ExceptionMessages.ModuleExecutionExceptionMessage(moduleName), e)
		{
		}
	}
}