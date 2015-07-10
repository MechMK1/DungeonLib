using System;

namespace MechMK1.DungeonLib.Old
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