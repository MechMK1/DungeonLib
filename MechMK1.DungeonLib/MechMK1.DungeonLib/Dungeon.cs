using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	public class Dungeon
	{
		public Tile[,] Tiles { get; private set; }

		public static Dungeon Create(int width, int height)
		{
			if (width < 2) throw new ArgumentOutOfRangeException("width", "Width must be at least 2");
			if (height < 2) throw new ArgumentOutOfRangeException("height", "Height must be at least 2");

			return new Dungeon(width, height);
		}

		private Dungeon(int width, int height)
		{
			Tuple<int, int> coords;
			Tile tmp;
			int level = 0;

			//Initialize tiles.
			this.Tiles = new Tile[width, height];

			//Pick the middle or one of the middle tiles
			Tuple<int, int> middle = this.GetMiddleTile(width, height);


			//Make initial tile
			coords = middle;
			tmp = new Tile();
			tmp.Level = level;
			tmp.Symbol = 'S';
			tmp.Doors = (Doors)13; //Up, down, left
			SetTile(tmp, coords.Item1, coords.Item2);
			ProcessTile(tmp); // Recursive Stuff
			SetEnd();
		}

		private void SetEnd()
		{
			List<Tile> endPieces = new List<Tile>();
			foreach (Tile t in Tiles)
			{
				if (t == null) continue;
				if (t.IsEndTile())
				{
					endPieces.Add(t);
				}
			}

			if (endPieces.Count > 0)
			{
				endPieces[Util.Random.Next(endPieces.Count)].Symbol = 'E';
			}
			else
			{
				Tile end = null;
				while (end == null)
				{
					end = this.Tiles[Util.Random.Next(Tiles.GetLength(0)), Util.Random.Next(Tiles.GetLength(1))]; // Select a random tile
				}
				end.Symbol = 'E';
			}
		}

		

		/// <summary>
		/// Recursively process all tiles.
		/// </summary>
		/// <param name="t"></param>
		private void ProcessTile(Tile t)
		{
			byte data = (byte)t.Doors;
			byte mask = 1;
			while (data != 0)
			{
				if ((data & 1) == 1)
				{
					
					switch ((Doors)mask)
					{
						case Doors.Up:
							ProcessDoor(Doors.Up, ()=>(t.Y == 0), Tuple.Create<int, int>(t.X, t.Y - 1), t);
							break;
						case Doors.Right:
							ProcessDoor(Doors.Right, () => (t.X == Tiles.GetLength(0) - 1), Tuple.Create<int, int>(t.X + 1, t.Y), t);
							break;
						case Doors.Down:
							ProcessDoor(Doors.Down, () => (t.Y == Tiles.GetLength(1) - 1), Tuple.Create<int, int>(t.X, t.Y + 1), t);
							break;
						case Doors.Left:
							ProcessDoor(Doors.Down, () => (t.X == 0), Tuple.Create<int, int>(t.X - 1, t.Y), t);
							break;
						default:
							throw new ArgumentException("I kinda fucked up here");
					}
				}

				mask <<= 1;
				data >>= 1;
			}
		}

		private void ProcessDoor(Doors door, Func<bool> endReached, Tuple<int, int> coords, Tile t)
		{
			Tile adj;
			if (endReached())
			{
				t.Doors &= ~door; //Remove up since we are already on the top
				return;
			}

			if ((adj = Tiles[coords.Item1, coords.Item2]) == null)
			{
				adj = new Tile();
				adj.Doors |= Util.GetOpposite(door);
				SetTile(adj, coords.Item1, coords.Item2);
				ProcessTile(adj);
			}
			else
			{
				adj.Doors |= Util.GetOpposite(door);
			}
		}
		private void SetTile(Tile tile, int x, int y)
		{
			this.Tiles[x, y] = tile;
			tile.X = x;
			tile.Y = y;
		}
		private Tuple<int, int> GetMiddleTile(int width, int height)
		{
			bool widthUneven = (width % 2 == 1);
			bool heightUneven = (height % 2 == 1);

			//Case 1: Both width and height are uneven. Only one middle tile
			if (widthUneven && heightUneven)
			{
				return Tuple.Create(
					(width / 2),
					(height / 2)
				);
			}

			//Case 2: Width is uneven, height is even. Two "middle" tiles. One is chosen randomly
			if (widthUneven && !heightUneven)
			{
				return Tuple.Create(
					(width / 2),
					(height / 2) - Util.Random.Next(2) // r.Next(2) returns either 0 or 1
				);
			}

			//Case 3: Width is even, height uneven. Same as Case 2
			if (!widthUneven && heightUneven)
			{
				return Tuple.Create(
					(width / 2) - Util.Random.Next(2), // r.Next(2) returns either 0 or 1
					(height / 2)
				);
			}

			//Case 4: Both are even
			return Tuple.Create(
					(width / 2) - Util.Random.Next(2), // r.Next(2) returns either 0 or 1
					(height / 2) - Util.Random.Next(2) // r.Next(2) returns either 0 or 1
			);
		}
	}
}
