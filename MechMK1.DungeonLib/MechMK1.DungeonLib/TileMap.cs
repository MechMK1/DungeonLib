using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	public class TileMap<T> : IEnumerable<T> where T : Tile
	{
		private T[,] tiles;

		public TileMap(int width, int height)
		{
			if (width < 2) throw new ArgumentOutOfRangeException("width", "Width must be at least 2");
			if (height < 2) throw new ArgumentOutOfRangeException("height", "Height must be at least 2");

			this.tiles = new T[width, height];
		}

		public int Width { get { return tiles.GetLength(0); } }
		public int Height { get { return tiles.GetLength(1); } }

		#region Indexer
		/// <summary>
		/// Get the tile for coordinates
		/// </summary>
		/// <param name="x">X-coordinate</param>
		/// <param name="y">Y-coordinate</param>
		/// <returns></returns>
		public T this[int x, int y]
		{
			get
			{
				return this.tiles[x, y];
			}
			internal set
			{
				this.tiles[x, y] = value;
				value.X = x;
				value.Y = y;
			}
		}
		#endregion

		#region Enumerators
		public IEnumerator<T> GetEnumerator()
		{
			foreach (T tile in this.tiles)
			{
				yield return tile;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		} 
		#endregion
	}
}
