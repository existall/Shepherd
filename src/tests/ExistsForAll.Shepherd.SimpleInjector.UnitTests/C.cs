using System.Collections.Generic;

namespace ExistsForAll.Shepherd.SimpleInjector.UnitTests
{
	class C : IC
	{
		private readonly IZx<int> _z;
		private readonly IEnumerable<IV> _enumerable;
		private readonly IEnumerable<IV<string>> _vs;

		public C(IZx<int> z, IEnumerable<IV> enumerable, IEnumerable<IV<string>> vs )
		{
			_z = z;
			_enumerable = enumerable;
			_vs = vs;

			foreach (var x in _enumerable)
			{

			}
		}
	}
}