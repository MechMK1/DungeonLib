using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib.TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Dungeon d = Dungeon.Create(15, 5);
			DisplayDungeon(d);
		}

		public static void DisplayDungeon(Dungeon d)
		{
			for (int h = 0; h < d.Tiles.GetLength(1); h++)
			{
				for (int w = 0; w < d.Tiles.GetLength(0); w++)
				{
					Tile t = d.Tiles[w, h];
					char toDraw = (t == null) ? ' ' : t.Symbol;
					Console.Write(toDraw);
				}
				Console.WriteLine();
			}
		}
	}
}
