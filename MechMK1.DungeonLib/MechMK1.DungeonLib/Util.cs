using System;

namespace MechMK1.DungeonLib.Old
{
	/// <summary>
	/// Internal Utility class
	/// </summary>
	internal static class Util
	{
		#region Internal Fields

		/// <summary>
		/// Static Random generator. Use the same random object in the entire code
		/// </summary>
		internal static readonly Random Random = debug ? new Random(5) : new Random();

		#endregion Internal Fields

		#region Private Fields

		/// <summary>
		/// Enables various debugging options
		/// </summary>
		private static bool debug =
#if DEBUG
 true;

#else
									false;
#endif

		#endregion Private Fields

		#region Internal Methods

		/// <summary>
		/// Returns the opposite direction.
		/// E.g. Up/Down, Left/Right
		/// </summary>
		/// <param name="door">Direction</param>
		/// <returns>Opposite direction</returns>
		internal static Doors GetOpposite(Doors door)
		{
			switch (door)
			{
				case Doors.Up:
					return Doors.Down;

				case Doors.Right:
					return Doors.Left;

				case Doors.Down:
					return Doors.Up;

				case Doors.Left:
					return Doors.Right;

				default:
					throw new ArgumentException("door", "Invalid value for parameter");
			}
		}

		#endregion Internal Methods
	}
}