using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Assets.Locations
{
    /// <summary>
    /// Represents a region.
    /// </summary>
    public sealed class Region : ExaminableObject
    {
        #region Fields

        private Room currentRoom;
        private readonly List<RoomPosition> roomPositions = new List<RoomPosition>();

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of rooms region contains.
        /// </summary>
        public int Rooms => roomPositions.Count;

        /// <summary>
        /// Get the current room.
        /// </summary>
        public Room CurrentRoom
        {
            get
            {
                if (currentRoom != null) 
                    return currentRoom;

                if (roomPositions.Count > 0)
                {
                    var first = roomPositions.First().Room;
                    SetStartRoom(first);
                    currentRoom = first;
                }

                return currentRoom;
            }
            private set { currentRoom = value; }
        }

        /// <summary>
        /// Get a room at a specified location.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z] => roomPositions.FirstOrDefault(r => r.IsAtPosition(x, y, z))?.Room;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>s
        /// <param name="identifier">This Regions identifier.</param>
        /// <param name="description">The description of this Region.</param>
        public Region(string identifier, string description) : this(new Identifier(identifier), new Description(description))
        {
        }

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>s
        /// <param name="identifier">This Regions identifier.</param>
        /// <param name="description">The description of this Region.</param>
        public Region(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the position of a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The position of the room.</returns>
        public RoomPosition GetPositionOfRoom(Room room)
        {
            var matrix = ToMatrix();

            if (matrix == null)
                return null;

            for (var z = 0; z < matrix.Depth; z++)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    for (var x = 0; x < matrix.Width; x++)
                    {
                        if (room == matrix[x, y, z])
                            return new RoomPosition(room, x, y, z);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Add a Room to this region.
        /// </summary>
        /// <param name="room">The room to add.</param>
        /// <param name="x">The x position within the region.</param>
        /// <param name="y">The y position within the region.</param>
        /// <param name="z">The z position within the region.</param>
        public bool AddRoom(Room room, int x, int y, int z)
        {
            var addable = !roomPositions.Any(r => r.IsAtPosition(x, y, z));

            if (addable)
                roomPositions.Add(new RoomPosition(room, x, y, z));

            return addable;
        }

        /// <summary>
        /// Get an adjoining room to the Region.CurrentRoom property.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <returns>The adjoining Room.</returns>
        public Room GetAdjoiningRoom(Direction direction)
        {
            return GetAdjoiningRoom(direction, CurrentRoom);
        }

        /// <summary>
        /// Get an adjoining room to a room.
        /// </summary>
        /// <param name="direction">The direction of the adjoining room.</param>
        /// <param name="room">The room to use as the reference.</param>
        /// <returns>The adjoining room.</returns>
        public Room GetAdjoiningRoom(Direction direction, Room room)
        {
            var roomPosition = roomPositions.FirstOrDefault(r => r.Room == room);

            if (roomPosition == null)
                return null;

            NextPosition(roomPosition.X, roomPosition.Y, roomPosition.Z, direction, out var x, out var y, out var z);
            return this[x, y, z];
        }

        /// <summary>
        /// Move in a direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        /// <returns>True if the move was successful, else false.</returns>
        public bool Move(Direction direction)
        {
            if (!CurrentRoom.CanMove(direction)) 
                return false;

            var adjoiningRoom = GetAdjoiningRoom(direction);

            if (adjoiningRoom == null)
                return false;

            adjoiningRoom.MovedInto(direction.Inverse());
            CurrentRoom = adjoiningRoom;

            return true;
        }

        /// <summary>
        /// Set the room to start in.
        /// </summary>
        /// <param name="room">The Room to start in.</param>
        public void SetStartRoom(Room room)
        {
            CurrentRoom = room;
            CurrentRoom.MovedInto(null);
        }

        /// <summary>
        /// Set the room to start in.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        public void SetStartRoom(int x, int y, int z)
        {
            var room = roomPositions.FirstOrDefault(r => r.IsAtPosition(x, y, z))?.Room;
            SetStartRoom(room ?? roomPositions.ElementAt(0).Room);
        }

        /// <summary>
        /// Unlock a pair of doors in a specified direction in the CurrentRoom.
        /// </summary>
        /// <param name="direction">The direction to unlock in.</param>
        /// <returns>True if the door pair could be unlocked, else false.</returns>
        public bool UnlockDoorPair(Direction direction)
        {
            var exitInThisRoom = CurrentRoom[direction];
            var roomPosition = roomPositions.FirstOrDefault(x => x.Room == CurrentRoom);

            if (roomPosition == null)
                return false;

            if (exitInThisRoom == null)
                return false;

            var adjoiningRoom = GetAdjoiningRoom(direction);

            if (adjoiningRoom == null)
                return false;

            if (!adjoiningRoom.FindExit(direction.Inverse(), true, out var exit))
                return false;

            if (exit == null)
                return false;

            exit.Unlock();
            exitInThisRoom.Unlock();
            return true;
        }

        /// <summary>
        /// Get this region as a 3D matrix of rooms.
        /// </summary>
        /// <returns>This region, as a 3D matrix.</returns>
        public Matrix ToMatrix()
        {
            return RegionMaker.ConvertToRoomMatrix(roomPositions);
        }

        /// <summary>
        /// Jump to a room.
        /// </summary>
        /// <param name="x">The x location of the room.</param>
        /// <param name="y">The y location of the room.</param>
        /// <param name="z">The z location of the room.</param>
        /// <returns>True if the room could be jumped to, else false.</returns>
        public bool JumpToRoom(int x, int y, int z)
        {
            var roomPosition = roomPositions.FirstOrDefault(r => r.IsAtPosition(x, y, z));

            if (roomPosition == null)
                return false;

            CurrentRoom = roomPosition.Room;

            return true;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the next position given a current position.
        /// </summary>
        /// <param name="x">The current X.</param>
        /// <param name="y">The current Y.</param>
        /// <param name="z">The current Z.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="nextX">The next X.</param>
        /// <param name="nextY">The next Y.</param>
        /// <param name="nextZ">The next Z.</param>
        internal static void NextPosition(int x, int y, int z, Direction direction, out int nextX, out int nextY, out int nextZ)
        {
            switch (direction)
            {
                case Direction.North:
                    nextX = x;
                    nextY = y + 1;
                    nextZ = z;
                    break;
                case Direction.East:
                    nextX = x + 1;
                    nextY = y;
                    nextZ = z;
                    break;
                case Direction.South:
                    nextX = x;
                    nextY = y - 1;
                    nextZ = z;
                    break;
                case Direction.West:
                    nextX = x - 1;
                    nextY = y;
                    nextZ = z;
                    break;
                case Direction.Up:
                    nextX = x;
                    nextY = y;
                    nextZ = z + 1;
                    break;
                case Direction.Down:
                    nextX = x;
                    nextY = y;
                    nextZ = z - 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public override ExaminationResult Examine()
        {
            return new ExaminationResult(Identifier + ": " + Description.GetDescription());
        }

        #endregion
    }
}