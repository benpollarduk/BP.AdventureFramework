using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;

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
        /// Get the current Room.
        /// </summary>
        public Room CurrentRoom
        {
            get
            {
                if (currentRoom != null) 
                    return currentRoom;

                if (roomPositions.Count > 0)
                    SetStartRoom(0);

                return currentRoom;
            }
            private set { currentRoom = value; }
        }

        /// <summary>
        /// Get a Room at a specified location.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns>The room.</returns>
        public Room this[int column, int row] => roomPositions.FirstOrDefault(r => r.IsAtPosition(column, row))?.Room;

        #endregion

        #region Constructors

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
        /// Add a Room to this Region.
        /// </summary>
        /// <param name="room">The Room to add.</param>
        /// <param name="column">The column within the region.</param>
        /// <param name="row">The row within the region.</param>
        public bool AddRoom(Room room, int column, int row)
        {
            var addable = !roomPositions.Any(r => r.IsAtPosition(column, row));

            if (addable)
                roomPositions.Add(new RoomPosition(room, column, row));

            return addable;
        }

        /// <summary>
        /// Get an adjoining Room to the Region.CurrentRoom property.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <returns>The adjoining Room, if there is one.</returns>
        public Room GetAdjoiningRoom(CardinalDirection direction)
        {
            return GetAdjoiningRoom(direction, CurrentRoom);
        }

        /// <summary>
        /// Get an adjoining Room to a Room.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <param name="room">The Room to start the check in.</param>
        /// <returns>The adjoining Room, if there is one.</returns>
        public Room GetAdjoiningRoom(CardinalDirection direction, Room room)
        {
            if (!room.CanMove(direction)) 
                return null;

            var roomPosition = roomPositions.FirstOrDefault(x => x.Room == room);

            if (roomPosition == null)
                throw new Exception("There was no room.");

            int column;
            int row;

            switch (direction)
            {
                case CardinalDirection.East:
                    column = roomPosition.X + 1;
                    row = roomPosition.Y;
                    break;
                case CardinalDirection.North:
                    column = roomPosition.X;
                    row = roomPosition.Y + 1;
                    break;
                case CardinalDirection.South:
                    column = roomPosition.X;
                    row = roomPosition.Y - 1;
                    break;
                case CardinalDirection.West:
                    column = roomPosition.X- 1;
                    row = roomPosition.Y;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return this[column, row];
        }

        /// <summary>
        /// Move in a direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        /// <returns>If a move was successful.</returns>
        public bool Move(CardinalDirection direction)
        {
            if (!CurrentRoom.CanMove(direction)) 
                return false;

            CurrentRoom = GetAdjoiningRoom(direction);
            CurrentRoom.MovedInto(direction.Inverse());

            return true;
        }

        /// <summary>
        /// Set the Room to start in.
        /// </summary>
        /// <param name="room">The Room to start in.</param>
        public void SetStartRoom(Room room)
        {
            CurrentRoom = room;
            CurrentRoom.MovedInto(null);
        }

        /// <summary>
        /// Set the Room to start in.
        /// </summary>
        /// <param name="index">The index of Room to start in.</param>
        public void SetStartRoom(int index)
        {
            SetStartRoom(roomPositions.ElementAt(index).Room);
        }

        /// <summary>
        /// Unlock a pair of doors in a specified direction in the CurrentRoom.
        /// </summary>
        /// <param name="direction">The direction to unlock in.</param>
        public void UnlockDoorPair(CardinalDirection direction)
        {
            var exitInThisRoom = CurrentRoom[direction];

            var roomPosition = roomPositions.FirstOrDefault(x => x.Room == CurrentRoom);

            if (roomPosition == null)
                throw new Exception("There was no room.");

            if (exitInThisRoom != null)
            {
                Exit exitInOpposingRoom;

                switch (direction)
                {
                    case CardinalDirection.East:
                        exitInOpposingRoom = this[roomPosition.X + 1, roomPosition.Y][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.North:
                        exitInOpposingRoom = this[roomPosition.X, roomPosition.Y + 1][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.South:
                        exitInOpposingRoom = this[roomPosition.X, roomPosition.Y - 1][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.West:
                        exitInOpposingRoom = this[roomPosition.X - 1, roomPosition.Y][(CardinalDirection)(-(int)direction)];
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (exitInOpposingRoom != null)
                {
                    exitInOpposingRoom.Unlock();
                    exitInThisRoom.Unlock();
                }
                else
                {
                    throw new Exception("There was no opposing exit.");
                }
            }
            else
            {
                throw new Exception("There exit in the current room in the specified direction.");
            }
        }

        /// <summary>
        /// Get this region as a 2D matrix of rooms.
        /// </summary>
        /// <returns>This region, as a 2D matrix.</returns>
        public Room[,] ToMatrix()
        {
            return RegionMaker.ConvertToRoomMatrix(roomPositions);
        }

        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public override ExaminationResult Examime()
        {
            return new ExaminationResult(Identifier + ": " + Description.GetDescription());
        }

        #endregion
    }
}