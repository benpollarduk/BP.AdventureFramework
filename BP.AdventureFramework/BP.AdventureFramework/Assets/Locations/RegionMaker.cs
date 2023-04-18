using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Provides a class for helping to make Regions.
    /// </summary>
    public class RegionMaker
    {
        #region Fields

        private readonly List<RoomPosition> rooms = new List<RoomPosition>();

        #endregion

        #region Properties

        /// <summary>
        /// Get the identifier.
        /// </summary>
        private Identifier Identifier { get; }

        /// <summary>
        /// Get the description.
        /// </summary>
        private Description Description { get; }

        /// <summary>
        /// Get or set the room at a location.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y]
        {
            get { return rooms.FirstOrDefault(r => r.IsAtPosition(x, y))?.Room; }
            set
            {
                var element = rooms.FirstOrDefault(r => r.IsAtPosition(x, y));

                if (element != null)
                    rooms.Remove(element);

                rooms.Add(new RoomPosition(value, x, y));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RegionMaker class.
        /// </summary>
        /// <param name="identifier">An identifier for the region.</param>
        /// <param name="description">A description for the region.</param>
        public RegionMaker(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <returns>The created region.</returns>
        public Region Make()
        {
            var region = new Region(Identifier, Description);

            var matrix = ConvertToRoomMatrix(rooms);

            for (var y = matrix.GetLowerBound(1); y < matrix.GetUpperBound(1); y++)
            {
                for (var x = matrix.GetLowerBound(0); x < matrix.GetUpperBound(0); x++)
                {
                    var room = matrix[x, y];

                    if (room != null)
                        region.AddRoom(room, x, y);
                }
            }

            ResolveExits(region);

            return region;
        }

        /// <summary>
        /// Clear all rooms.
        /// </summary>
        public void Clear()
        {
            rooms.Clear();
        }

        #endregion

        #region StaticMethods

        private static Region ResolveExits(Region region)
        {
            foreach (var room in region.ToMatrix())
            {
                foreach (var direction in new[] { CardinalDirection.North, CardinalDirection.East, CardinalDirection.South, CardinalDirection.West })
                {
                    if (!room.FindExit(direction, true, out _)) 
                        continue;

                    var adjoining = region.GetAdjoiningRoom(direction);
                    var inverse = direction.Inverse();

                    if (adjoining != null && !adjoining.FindExit(inverse, true, out var exit))
                        adjoining.AddExit(new Exit(inverse, exit.IsLocked));
                }
            }

            return region;
        }

        /// <summary>
        /// Convert region to a 2D matrix of rooms.
        /// </summary>
        /// <returns>A 2D matrix.</returns>
        public static Room[,] ConvertToRoomMatrix(IReadOnlyCollection<RoomPosition> roomPositions)
        {
            var minX = roomPositions.Min(x => x.X);
            var minY = roomPositions.Min(x => x.Y);
            var maxX = roomPositions.Max(x => x.X);
            var maxY = roomPositions.Max(x => x.Y);

            var lengthX = maxX - minX;
            var lengthY = maxY - minY;

            var xNormalisationOffset = 0 - minX;
            var yNormalisationOffset = 0 - minY;

            var matrix = new Room[lengthX, lengthY];

            foreach (var roomPosition in roomPositions)
                matrix[roomPosition.X + xNormalisationOffset, roomPosition.Y + yNormalisationOffset] = roomPosition.Room;

            return matrix;
        }

        #endregion
    }
}
