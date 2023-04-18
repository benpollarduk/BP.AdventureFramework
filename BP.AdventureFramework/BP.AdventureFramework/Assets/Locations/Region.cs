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
                    SetStartRoom(roomPositions.First().Room);

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
                return null;

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

            var adjoiningRoom = GetAdjoiningRoom(direction);

            if (adjoiningRoom == null)
                return false;

            adjoiningRoom.MovedInto(direction.Inverse());
            CurrentRoom = adjoiningRoom;

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
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        public void SetStartRoom(int column, int row)
        {
            var room = roomPositions.FirstOrDefault(x => x.IsAtPosition(column, row))?.Room;
            SetStartRoom(room ?? roomPositions.ElementAt(0).Room);
        }

        /// <summary>
        /// Unlock a pair of doors in a specified direction in the CurrentRoom.
        /// </summary>
        /// <param name="direction">The direction to unlock in.</param>
        /// <returns>True if the door pair could be unlocked, else false.</returns>
        public bool UnlockDoorPair(CardinalDirection direction)
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