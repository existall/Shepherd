using System;
using ExistsForAll.Shepherd.Core.Resources;

namespace ExistsForAll.Shepherd.Core
{
	internal class ModuleExecutionException : Exception
	{
		public ModuleExecutionException(string moduleName, Exception e)
			: base(ExceptionMessages.ModuleExecutionExceptionMessage(moduleName), e)
		{
		}
	}
}