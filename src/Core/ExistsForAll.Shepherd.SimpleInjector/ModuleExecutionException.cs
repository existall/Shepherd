using System;
using ExistsForAll.Shepherd.SimpleInjector.Resources;

namespace ExistsForAll.Shepherd.SimpleInjector
{
	internal class ModuleExecutionException : Exception
	{
		public ModuleExecutionException(string moduleName, Exception e)
			: base(ExceptionMessages.ModuleExecutionExceptionMessage(moduleName), e)
		{
		}
	}
}