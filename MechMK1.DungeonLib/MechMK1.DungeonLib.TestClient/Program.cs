using System;

namespace MechMK1.DungeonLib.TestClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			
		}

		public static void DisplayDungeon(Dungeon d)
		{
			for (int h = 0; h < d.Tiles.Height; h++)
			{
				for (int w = 0; w < d.Tiles.Width; w++)
				{
					Tile t = d.Tiles[w, h];
					char toDraw = (t == null) ? ' ' : (t.MetaSymbol ?? t.Symbol);
					Console.Write(toDraw);
				}
				Console.WriteLine();
			}
		}
	}
}