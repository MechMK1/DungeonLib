namespace MechMK1.DungeonLib
{
	public class Tile
	{
		#region Private Fields

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

		/// <summary>
		/// A Flag-field indicating which doors a tile has
		/// </summary>
		private Doors doors;

		#endregion Private Fields

		#region Public Constructors

		/// <summary>
		/// Default constructor. Sets Doors to a random value.
		/// </summary>
		public Tile()
		{
			this.Doors = GetRandomDoors();
		}

		#endregion Public Constructors

		#region Public Properties

		/// <summary>
		/// Property to access the Doors of a dungeon. The symbols are updated whenever the doors are modified.
		/// </summary>
		public Doors Doors
		{
			get { return doors; }
			set { doors = value; UpdateSymbols(); }
		}

		/// <summary>
		/// Character which overrides the normal symbol if set (e.g. Start, Exit, etc...)
		/// </summary>
		public char? MetaSymbol { get; set; }

		/// <summary>
		/// Character which represents the tile
		/// </summary>
		public char Symbol { get; set; } //TODO Change char to something more generic, like IDisplayable or such

		/// <summary>
		/// Helper property which represents the coordinates of this tile
		/// </summary>
		public int X { get; internal set; } //TODO Implement TileMap class to unify this

		/// <summary>
		/// Helper property which represents the coordinates of this tile
		/// </summary>
		public int Y { get; internal set; } //TODO Implement TileMap class to unify this

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

		/// <summary>
		/// Set the symbol according to the new set of doors
		/// </summary>
		private void UpdateSymbols()
		{
			this.Symbol = syms[((byte)this.Doors) - 1];
		}

		#endregion Private Methods
	}
}