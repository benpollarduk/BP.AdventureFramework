namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents a room position.
    /// </summary>
    public class RoomPosition
    {
        #region Properties

        /// <summary>
        /// Get the room.
        /// </summary>
        public Room Room { get; }

        /// <summary>
        /// Get the X position of the room.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Get the Y position of the room.
        /// </summary>
        public int Y { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoomPosition class.
        /// </summary>
        /// <param name="room">The room/</param>
        /// <param name="x">The x position of the room.</param>
        /// <param name="y">The y position of the room.</param>
        public RoomPosition(Room room, int x, int y)
        {
            Room = room;
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get if this RoomPosition is at a position.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <returns>True if this is at the position, else false.</returns>
        public bool IsAtPosition(int x, int y)
        {
            
            return X == x && Y == y;
        }

        #endregion
    }
}
