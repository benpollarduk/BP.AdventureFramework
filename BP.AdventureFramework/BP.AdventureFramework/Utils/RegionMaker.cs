using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Utils
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
        public RegionMaker(string identifier, string description) : this(new Identifier(identifier), new Description(description))
        {
        }

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
            var firstRoom = rooms.First();
            return Make(firstRoom.X, firstRoom.Y);
        }

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <param name="column">The start column.</param>
        /// <param name="row">The start row.</param>
        /// <returns>The created region.</returns>
        public Region Make(int column, int row)
        {
            var region = new Region(Identifier, Description);

            var matrix = ConvertToRoomMatrix(rooms);

            for (var y = matrix.GetLowerBound(1); y < matrix.GetLength(1); y++)
            {
                for (var x = matrix.GetLowerBound(0); x < matrix.GetLength(0); x++)
                {
                    var room = matrix[x, y];

                    if (room != null)
                        region.AddRoom(room, x, y);
                }
            }

            LinkExits(region);

            region.SetStartRoom(column, row);

            return region;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Ensure all rooms within a region have exits that are linked to adjacent rooms.
        /// </summary>
        /// <param name="region">The region.</param>
        internal static void LinkExits(Region region)
        {
            foreach (var room in region.ToMatrix())
            {
                if (room == null)
                    continue;

                foreach (var direction in new[] { CardinalDirection.North, CardinalDirection.East, CardinalDirection.South, CardinalDirection.West })
                {
                    if (!room.FindExit(direction, true, out var exit)) 
                        continue;

                    var adjoining = region.GetAdjoiningRoom(direction);
                    var inverse = direction.Inverse();

                    if (adjoining != null && !adjoining.FindExit(inverse, true, out _))
                        adjoining.AddExit(new Exit(inverse, exit.IsLocked));
                }
            }
        }

        /// <summary>
        /// Convert region to a 2D matrix of rooms.
        /// </summary>
        /// <returns>A 2D matrix.</returns>
        internal static Room[,] ConvertToRoomMatrix(IReadOnlyCollection<RoomPosition> roomPositions)
        {
            if (roomPositions == null || roomPositions.Count == 0)
                return null;

            var minX = roomPositions.Min(x => x.X);
            var minY = roomPositions.Min(x => x.Y);
            var maxX = roomPositions.Max(x => x.X);
            var maxY = roomPositions.Max(x => x.Y);

            var lengthX = (maxX - minX) + 1;
            var lengthY = (maxY - minY) + 1;

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