using System;

namespace MechMK1.DungeonLib.TestClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Dungeon<ASCIITile> d = Dungeon<ASCIITile>.Create(10, 10);
			DisplayDungeon(d);
		}

		public static void DisplayDungeon(Dungeon<ASCIITile> d)
		{
			for (int h = 0; h < d.Tiles.Height; h++)
			{
				
			}

			for (int h = 0; h < d.Tiles.Height; h++)
			{
				for (int w = 0; w < d.Tiles.Width; w++)
				{
					ASCIITile t = d.Tiles[w, h];
					if (t != null) t.Draw();
					else Console.Write(' ');
				}
				Console.WriteLine();
			}
		}
	}
}