using System;
using System.Runtime.InteropServices;
using SimpleInjector;

namespace ExistsForAll.Shepherd.SimpleInjector.Experimental
{
	public static class Bbb
	{
		public static Container C(this Container target)
		{
			var temp = target.Copy();
			target.Dispose();
			//BBB.Replace(temp,target);
			X(ref target, temp);
			//target.Dispose();
			//target = temp.re;
			return target;
		}

		public static void X(ref Container c, Container z)
		{
			c = z;
		}

		public static void Replace<T>(T x, T y)
			where T : class
		{
			// replaces 'x' with 'y'
			if (x == null) throw new ArgumentNullException("x");
			if (y == null) throw new ArgumentNullException("y");

			var size = Marshal.SizeOf(typeof(T));
			var ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(y, ptr, false);
			Marshal.PtrToStructure(ptr, x);
			Marshal.FreeHGlobal(ptr);
		}
	}
}