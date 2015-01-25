namespace MechMK1.DungeonLib
{
	public abstract class Tile
	{
		#region Public Constructors
		/// <summary>
		/// Default constructor. Sets Doors to a random value.
		/// </summary>
		public Tile()
		{
			this.Doors = GetRandomDoors();
			this.TileInfo = new TileInfo();
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Property to access the Doors of a dungeon. The symbols are updated whenever the doors are modified.
		/// </summary>
		public Doors Doors { get; set; }

		/// <summary>
		/// Various information about the content of the tile, e.g. if it is the start tile, etc...
		/// </summary>
		public TileInfo TileInfo { get; private set; }

		/// <summary>
		/// Helper property which represents the coordinates of this tile
		/// </summary>
		public int X { get; internal set; }

		/// <summary>
		/// Helper property which represents the coordinates of this tile
		/// </summary>
		public int Y { get; internal set; }
		#endregion Public Properties

		#region Public Methods
		/// <summary>
		/// Determines if a tile is a dead-end (only has one adjecant and traversable tile)
		/// </summary>
		/// <returns>True if room has 1 door, otherwise false</returns>
		public bool IsEndTile()
		{
			return this.NumberOfDoors() == 1;
		}

		/// <summary>
		/// Determines the number of doors of a tile
		/// </summary>
		/// <returns>Number of doors of a tile</returns>
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
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Creates a random combination of doors.
		/// Note: The doors are distributed evenly among every combination of doors, not the number of doors.
		/// The number of doors are distributed as follows: 26.7 %, 40 %, 26.7 %, 6.7%
		/// </summary>
		/// <returns></returns>
		private Doors GetRandomDoors()
		{
			return (Doors)Util.Random.Next(1, 16);
		}
		#endregion Private Methods

		#region Abstract Methods
		/// <summary>
		/// Draw the tile in a way the inherited class chooses.
		/// </summary>
		public abstract void Draw();
		#endregion Abstract Methods
	}
}