using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechMK1.DungeonLib
{
	public class Tile
	{
		private static Random r = new Random();
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
		public Tile()
		{
			this.Doors = GetRandomDoors();
			this.Symbol = '#';
		}

		public int Level { get; set; }
		public char Symbol { get; set; }
		private Doors doors;

		public Doors Doors
		{
			get { return doors; }
			set { doors = value; UpdateSymbols(); }
		}

		private void UpdateSymbols()
		{
			if (this.Symbol != 'S' && this.Symbol != 'E')
			{
				this.Symbol = syms[((byte)this.Doors) - 1];
			}
		}

		public int X { get; set; }
		public int Y { get; set; }

		public int NumberOfDoors()
		{
			int value = (byte)this.Doors;
			int count = 0;
			while (value != 0)
			{
				count++;
				value &= value - 1;
			}
			return count;
		}

		public bool IsEndTile()
		{
			return this.NumberOfDoors() == 1;
		}

		private Doors GetRandomDoors()
		{
			return (Doors)r.Next(1, 16);
		}
	}
}
