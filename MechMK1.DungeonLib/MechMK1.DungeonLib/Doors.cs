using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	[Flags]
	public enum Doors : byte
	{
		Up = 1,
		Right = 2,
		Down = 4,
		Left = 8
	}
}
