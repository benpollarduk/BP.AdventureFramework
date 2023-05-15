using System.Collections.Generic;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Provides a 3D matrix of rooms.
    /// </summary>
    public sealed class Matrix
    {
        #region Fields

        private readonly Room[,,] rooms;

        #endregion

        #region Properties

        /// <summary>
        /// Get a room in this matrix.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z] => rooms[x, y, z];

        /// <summary>
        /// Get the width of the matrix.
        /// </summary>
        public int Width => rooms.GetLength(0);

        /// <summary>
        /// Get the height of the matrix.
        /// </summary>
        public int Height => rooms.GetLength(1);

        /// <summary>
        /// Get the depth of the matrix.
        /// </summary>
        public int Depth => rooms.GetLength(2);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Matrix class.
        /// </summary>
        /// <param name="rooms">The rooms to be represented.</param>
        public Matrix(Room[,,] rooms)
        {
            this.rooms = rooms;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return this matrix as a one dimensional array of rooms.
        /// </summary>
        /// <returns>The rooms, as a one dimensional array.</returns>
        public Room[] ToRooms()
        {
            var roomList = new List<Room>();

            for (var z = 0; z < Depth; z++)
            {
                for (var y = 0; y < Height; y++)
                {
                    for (var x = 0; x < Width; x++)
                    {
                        if (this[x, y, z] != null)
                            roomList.Add(this[x, y, z]);
                    }
                }
            }

            return roomList.ToArray();
        }

        #endregion
    }
}
