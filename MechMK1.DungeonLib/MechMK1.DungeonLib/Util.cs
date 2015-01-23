using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	/// <summary>
	/// Internal Utility class
	/// </summary>
	static class Util
	{
		/// <summary>
		/// Enables various debugging options
		/// </summary>
		private static bool debug = 
#if DEBUG
			true;
#else
			false;
#endif
		/// <summary>
		/// Static r
		/// </summary>
		internal static readonly Random Random = debug ? new Random(1) : new Random(); 
	}
}
