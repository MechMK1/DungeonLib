using System;
using System.Collections.Generic;

namespace MechMK1.DungeonLib.Old
{
	/// <summary>
	/// Represents an entire dungeon with all tiles and their coordinates
	/// </summary>
	public class Dungeon<T> where T : Tile, new()
	{
		#region Private Constructors

		/// <summary>
		/// Constructs a new Dungeon. Constructor is private to prevent overzealous dungeon construction
		/// </summary>
		/// <param name="width">Width of the dungeon in tiles. Must be at least 2</param>
		/// <param name="height">Height of the dungeon in tiles. Must be at least 2</param>
		private Dungeon(int width, int height)
		{
			//Initialize tiles.
			this.Tiles = new TileMap<T>(width, height);

			//Pick the middle or one of the middle tiles
			Tuple<int, int> middle = this.GetMiddleTile(width, height);

			//Make initial tile
			T tmp = new T();

			//Mark tile as StartTile
			tmp.TileInfo.Navigation = NavigationTile.Start;

			//Put the tile on the map
			this.Tiles[middle.Item1, middle.Item2] = tmp; ;

			//Recursively generate more tiles according to the start doors
			ProcessTile(tmp); // <--------------------------------------------------------------- The magic happens here

			//Find a suitable tile for an exit and set it
			SetExit();
		}

		#endregion Private Constructors
		
		#region Public Properties

		public TileMap<T> Tiles { get; private set; }

		#endregion Public Properties

		#region Public

		/// <summary>
		/// Creates a new Dungeon. This is a cpu-intense task compared to instantiation of a normal class.
		/// </summary>
		/// <param name="width">Width of the dungeon in tiles. Must be at least 2</param>
		/// <param name="height">Height of the dungeon in tiles. Must be at least 2</param>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when either width or height are less than 2</exception>
		/// <returns>The newly created Dungeon</returns>
		public static Dungeon<T> Create(int width, int height)
		{
			if (width < 2) throw new ArgumentOutOfRangeException("width", "Width must be at least 2");
			if (height < 2) throw new ArgumentOutOfRangeException("height", "Height must be at least 2");

			return new Dungeon<T>(width, height);
		}

		#endregion Public

		#region Private Methods

		/// <summary>
		/// Recursively process a tile
		/// </summary>
		/// <param name="t">The currently processed tile</param>
		private void ProcessTile(Tile t)
		{
			byte data = (byte)t.Doors; //A 4-bit combination of the set doors. E.g. 1010 => left, right
			byte mask = 1;             //The "current" door in the loop

			while (data != 0) // When data was processed 4 times
			{
				if ((data & 1) == 1) //If the right-most tile is 1 (set)...
				{
					switch ((Doors)mask) //...check the value of mask.
					{
						case Doors.Up:
							ProcessDoor((Doors)mask, () => (t.Y == 0), Tuple.Create<int, int>(t.X, t.Y - 1), t);
							break;

						case Doors.Right:
							ProcessDoor((Doors)mask, () => (t.X == Tiles.Width - 1), Tuple.Create<int, int>(t.X + 1, t.Y), t);
							break;

						case Doors.Down:
							ProcessDoor((Doors)mask, () => (t.Y == Tiles.Height - 1), Tuple.Create<int, int>(t.X, t.Y + 1), t);
							break;

						case Doors.Left:
							ProcessDoor((Doors)mask, () => (t.X == 0), Tuple.Create<int, int>(t.X - 1, t.Y), t);
							break;

						default:
							throw new ArgumentException("I kinda fucked up here"); // This should never happen
					}
				}

				mask <<= 1;
				data >>= 1;
			}
		}

		/// <summary>
		/// Recursively process a door. Private function. May only be called by ProcessTile()
		/// </summary>
		/// <param name="door">Direction of the door</param>
		/// <param name="endReached">Condition when the outer borders of the map are reached</param>
		/// <param name="coords">Coordinates of the possibly adjecant tile</param>
		/// <param name="t">Current tile</param>
		private void ProcessDoor(Doors door, Func<bool> endReached, Tuple<int, int> coords, Tile t)
		{
			T adj;

			//If the outer borders of the map were reaches and the door points in this direction
			if (endReached())
			{
				t.Doors &= ~door; //Remove the door since we are already on the border
				return; //Then return because there is nothing left to do here
			}

			//If the adjecant tile does not yet exist...
			if ((adj = Tiles[coords.Item1, coords.Item2]) == null)
			{
				adj = new T(); //...we create it...
				adj.Doors |= Util.GetOpposite(door);//...and ensure they at least have a connection to us
				this.Tiles[coords.Item1, coords.Item2] = adj; //Then we place it on the map
				ProcessTile(adj); //And process this tile next
			}
			///Otherwise, an adjecant room exists
			else
			{
				adj.Doors |= Util.GetOpposite(door); //So we ensure that they both are connected
			}
		}
		/// <summary>
		/// Finds a suitable piece to create the exit on, then put the exit there
		/// </summary>
		private void SetExit()
		{
			// List for all possible end pieces
			List<Tile> endPieces = new List<Tile>();
			foreach (Tile t in Tiles)
			{
				if (t == null) continue; //Since Tiles is a two-dimmendsional array, it's likely to contain null's. Skip those.
				if (t.IsEndTile()) //Aka. only has one door
				{
					endPieces.Add(t); //Mark it as suitable
				}
			}

			if (endPieces.Count > 0) //If at least one end tile was found
			{
				endPieces
				[
					Util.Random.Next(endPieces.Count) //Pick one at random
				]
				.TileInfo.Navigation = NavigationTile.Exit; // And mark it as the exit
			}
			else //If no end pieces were found (e.g. the outter walls were a large ring structure), pick pieces at random
			{
				Tile end = null;
				do
				{
					end = this.Tiles[Util.Random.Next(Tiles.Width), Util.Random.Next(Tiles.Height)]; // Select a random tile
				} while (end == null || end.TileInfo.IsStart); //If tile is empty or tile is start, repeat

				
				end.TileInfo.Navigation = NavigationTile.Exit;
			}
		}

		#endregion Private Methods

		#region Helpers

		/// <summary>
		/// Gets the coordinates of the middle piece, or one of them at random if there are more middle pieces
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		private Tuple<int, int> GetMiddleTile(int width, int height)
		{
			bool widthUneven = (width % 2 == 1); //Is the width an odd number? (3, 5, 7, etc.)
			bool heightUneven = (height % 2 == 1); //Is the height an odd number? (3, 5, 7, etc.)

			//Case 1: Both width and height are odd. Only one middle tile
			if (widthUneven && heightUneven)
			{
				return Tuple.Create(
					(width / 2),
					(height / 2)
				);
			}

			//Case 2: Width is odd, height is even. Two "middle" tiles. One is chosen randomly
			if (widthUneven && !heightUneven)
			{
				return Tuple.Create(
					(width / 2),
					(height / 2) - Util.Random.Next(2) // r.Next(2) returns either 0 or 1
				);
			}

			//Case 3: Width is even, height odd. Same as Case 2
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

		#endregion Helpers
	}
}