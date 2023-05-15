using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Utilities
{
    /// <summary>
    /// Provides a class for helping to make Regions.
    /// </summary>
    public sealed class RegionMaker
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
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z]
        {
            get { return rooms.FirstOrDefault(r => r.IsAtPosition(x, y, z))?.Room; }
            set
            {
                var element = rooms.FirstOrDefault(r => r.IsAtPosition(x, y, z));

                if (element != null)
                    rooms.Remove(element);

                rooms.Add(new RoomPosition(value, x, y, z));
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
            return Make(firstRoom.X, firstRoom.Y, firstRoom.Z);
        }

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <param name="x">The start x position.</param>
        /// <param name="y">The start y position.</param>
        /// <param name="z">The start z position.</param>
        /// <returns>The created region.</returns>
        public Region Make(int x, int y, int z)
        {
            var region = new Region(Identifier, Description);

            var matrix = ConvertToRoomMatrix(rooms);

            for (var depth = 0; depth < matrix.Depth; depth++)
            {
                for (var row = 0; row < matrix.Height; row++)
                {
                    for (var column = 0; column < matrix.Width; column++)
                    {
                        var room = matrix[column, row, depth];

                        if (room != null)
                            region.AddRoom(room, column, row, depth);
                    }
                }
            }

            LinkExits(region);

            region.SetStartRoom(x, y, z);

            return region;
        }

        /// <summary>
        /// Determine if a room can be placed at a location
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="z">The Z position.</param>
        /// <returns>True if the room can be placed, else false.</returns>
        public bool CanPlaceRoom(int x, int y, int z)
        {
            return rooms.All(r => !r.IsAtPosition(x, y, z));
        }

        /// <summary>
        /// Get all current room positions.
        /// </summary>
        /// <returns>The room positions.</returns>
        public RoomPosition[] GetRoomPositions()
        {
            return rooms.ToArray();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Ensure all rooms within a region have exits that are linked to adjacent rooms.
        /// </summary>
        /// <param name="region">The region.</param>
        internal static void LinkExits(Region region)
        {
            foreach (var room in region.ToMatrix().ToRooms())
            {
                if (room == null)
                    continue;

                foreach (var direction in new[] { Direction.North, Direction.East, Direction.South, Direction.West })
                {
                    if (!room.FindExit(direction, true, out var exit)) 
                        continue;

                    var adjoining = region.GetAdjoiningRoom(direction, room);
                    var inverse = direction.Inverse();

                    if (adjoining != null && !adjoining.FindExit(inverse, true, out _))
                        adjoining.AddExit(new Exit(inverse, exit.IsLocked));
                }
            }
        }

        /// <summary>
        /// Convert region to a 3D matrix of rooms.
        /// </summary>
        /// <returns>A 3D matrix.</returns>
        internal static Matrix ConvertToRoomMatrix(IReadOnlyCollection<RoomPosition> roomPositions)
        {
            if (roomPositions == null || roomPositions.Count == 0)
                return null;

            var minX = roomPositions.Min(x => x.X);
            var minY = roomPositions.Min(x => x.Y);
            var minZ = roomPositions.Min(x => x.Z);
            var maxX = roomPositions.Max(x => x.X);
            var maxY = roomPositions.Max(x => x.Y);
            var maxZ = roomPositions.Max(x => x.Z);

            var lengthX = (maxX - minX) + 1;
            var lengthY = (maxY - minY) + 1;
            var lengthZ = (maxZ - minZ) + 1;

            var xNormalisationOffset = 0 - minX;
            var yNormalisationOffset = 0 - minY;
            var zNormalisationOffset = 0 - minZ;

            var matrix = new Room[lengthX, lengthY, lengthZ];

            foreach (var roomPosition in roomPositions)
                matrix[roomPosition.X + xNormalisationOffset, roomPosition.Y + yNormalisationOffset, roomPosition.Z + zNormalisationOffset] = roomPosition.Room;

            return new Matrix(matrix);
        }

        #endregion
    }
}