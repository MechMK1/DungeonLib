using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	public class ASCIITile : Tile
	{
		/// <summary>
		/// Assign each combination of doors a character from the Unicode border characters. I.e. Up and Right are represented by '└'
		/// </summary>
		private static char[] syms = new char[]
		{
			'o',
			'o',
			'└',
			'o',
			'│',
			'┌',
			'├',
			'o',
			'┘',
			'─',
			'┴',
			'┐',
			'┤',
			'┬',
			'┼'
		};

		public override void Draw()
		{
			switch (this.TileInfo.Navigation)
			{
				case NavigationTile.None: // Tile is normal
					Console.Write(syms[((byte)this.Doors) - 1]);
					break;
				case NavigationTile.Start: // Tile is start
					Console.Write('S');
					break;
				case NavigationTile.Exit:
					Console.Write('E');
					break;
				default:
					throw new ArgumentOutOfRangeException("TileInfo.Navigation has unknown value");
			}
			
		}
	}
}
