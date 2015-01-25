using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechMK1.DungeonLib
{
	/*
	 * Design directive: Make internally setable properties to store actual data and read-only properties for easy access
	 */

	public class TileInfo
	{
		public NavigationTile Navigation { get; internal set; }
		public bool IsStart { get { return this.Navigation == DungeonLib.NavigationTile.Start; } }
		public bool IsExit { get { return this.Navigation == DungeonLib.NavigationTile.Exit; } }
	}

	public enum NavigationTile : int
	{
		None = 0,
		Start = 1,
		Exit = 2
	}
}
