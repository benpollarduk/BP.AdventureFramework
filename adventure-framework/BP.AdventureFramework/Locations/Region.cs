using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Locations
{
    /// <summary>
    /// Represents a region.
    /// </summary>
    public class Region : GameLocation
    {
        #region Fields

        private Room currentRoom;

        #endregion

        #region Properties

        /// <summary>
        /// Get the Rooms in this Region.
        /// </summary>
        public List<Room> Rooms { get; protected set; }

        /// <summary>
        /// Get the current Room.
        /// </summary>
        public Room CurrentRoom
        {
            get
            {
                if (currentRoom != null) 
                    return currentRoom;

                if (Rooms.Any())
                    SetStartRoom(0);

                return currentRoom;
            }
            protected set { currentRoom = value; }
        }

        /// <summary>
        /// Get the first Room found at a specified location.
        /// </summary>
        /// <param name="column">The column of the Room.</param>
        /// <param name="row">The row of the Room.</param>
        /// <returns>The adjoining room.</returns>
        public Room this[int column, int row]
        {
            get
            {
                var roomsInLocation = Rooms.Where(r => r.Column == column && r.Row == row).ToArray();
                return roomsInLocation.Length > 0 ? roomsInLocation[0] : null;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>
        /// <param name="name">The name of this Region.</param>
        /// <param name="description">The description of this Region.</param>
        public Region(string name, string description)
        {
            Name = name;
            Description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>
        /// <param name="name">The name of this Region.</param>
        /// <param name="description">The description of this Region.</param>
        public Region(string name, Description description)
        {
            Name = name;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create a Room in this Region.
        /// </summary>
        /// <param name="room">The Room to create.</param>
        /// <param name="columnInRegion">The column of the Room with this Region.</param>
        /// <param name="rowInRegion">The row of the Room within this Region.</param>
        public bool CreateRoom(Room room, int columnInRegion, int rowInRegion)
        {
            room.Column = columnInRegion;
            room.Row = rowInRegion;

            var addable = Rooms.All(r => r.Column != room.Column || r.Row != room.Row);

            if (addable)
                Rooms.Add(room);

            return addable;
        }

        /// <summary>
        /// Create a Room in this Region.
        /// </summary>
        /// <param name="room">The Room to create.</param>
        /// <param name="relativeLocation">The direction this Room lies in relative to the last Room created.</param>
        public bool CreateRoom(Room room, CardinalDirection relativeLocation)
        {
            room.Column = Rooms.Any() ? Rooms[Rooms.Count - 1].Column : 0;
            room.Row = Rooms.Any() ? Rooms[Rooms.Count - 1].Row : 0;

            if (Rooms.Any())
            {
                switch (relativeLocation)
                {
                    case CardinalDirection.East:
                        room.Column++;
                        break;
                    case CardinalDirection.North:
                        room.Row++;
                        break;
                    case CardinalDirection.South:
                        room.Row--;
                        break;
                    case CardinalDirection.West:
                        room.Column--;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            var addable = Rooms.All(r => r.Column != room.Column || r.Row != room.Row);

            if (addable)
                Rooms.Add(room);

            return addable;
        }

        /// <summary>
        /// Get an adjoining Room to the Region.CurrentRoom property.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <returns>The adjoining Room, if there is one.</returns>
        public virtual Room GetAdjoiningRoom(CardinalDirection direction)
        {
            return GetAdjoiningRoom(direction, CurrentRoom);
        }

        /// <summary>
        /// Get an adjoining Room to a Room.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <param name="startRoom">The Room to start the check in.</param>
        /// <returns>The adjoining Room, if there is one.</returns>
        public virtual Room GetAdjoiningRoom(CardinalDirection direction, Room startRoom)
        {
            if (!startRoom.CanMove(direction)) 
                return null;

            int column;
            int row;

            switch (direction)
            {
                case CardinalDirection.East:
                    column = startRoom.Column + 1;
                    row = startRoom.Row;
                    break;
                case CardinalDirection.North:
                    column = startRoom.Column;
                    row = startRoom.Row + 1;
                    break;
                case CardinalDirection.South:
                    column = startRoom.Column;
                    row = startRoom.Row - 1;
                    break;
                case CardinalDirection.West:
                    column = startRoom.Column - 1;
                    row = startRoom.Row;
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
            CurrentRoom.MovedInto((CardinalDirection)(-(int)direction));

            return true;
        }

        /// <summary>
        /// Move to a Room.
        /// </summary>
        /// <param name="roomName">The name of the Room to move to.</param>
        /// <returns>If a move was successful.</returns>
        public bool Move(string roomName)
        {
            var matches = Rooms.Where(r => r.Name == roomName).ToArray();
            return matches.Any() && Move(matches.ElementAt(0));
        }

        /// <summary>
        /// Move to a room.
        /// </summary>
        /// <param name="room">The room to move to.</param>
        /// <returns>If a move was successful.</returns>
        public bool Move(Room room)
        {
            if (!Rooms.Contains(room)) 
                return false;

            var movedCorrectly = TryGetDirectionOfAdjoiningRoom(CurrentRoom, room, out var direction);

            CurrentRoom = room;

            if (movedCorrectly)
                CurrentRoom.MovedInto((CardinalDirection)(-(int)direction));
            else
                CurrentRoom.MovedInto(null);

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
            SetStartRoom(Rooms[index]);
        }

        /// <summary>
        /// Unlock a pair of doors in a specified direction in the CurrentRoom.
        /// </summary>
        /// <param name="direction">The direction to unlock in.</param>
        public void UnlockDoorPair(CardinalDirection direction)
        {
            var exitInThisRoom = CurrentRoom[direction];

            if (exitInThisRoom != null)
            {
                Exit exitInOpposingRoom;

                switch (direction)
                {
                    case CardinalDirection.East:
                        exitInOpposingRoom = this[CurrentRoom.Column + 1, CurrentRoom.Row][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.North:
                        exitInOpposingRoom = this[CurrentRoom.Column, CurrentRoom.Row + 1][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.South:
                        exitInOpposingRoom = this[CurrentRoom.Column, CurrentRoom.Row - 1][(CardinalDirection)(-(int)direction)];
                        break;
                    case CardinalDirection.West:
                        exitInOpposingRoom = this[CurrentRoom.Column - 1, CurrentRoom.Row][(CardinalDirection)(-(int)direction)];
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
                    throw new Exception("There was no opposing exit");
                }
            }
            else
            {
                throw new Exception("There exit in the current room in the specified direction");
            }
        }

        /// <summary>
        /// Try and get the direction of an adjoining room.
        /// </summary>
        /// <param name="sourceRoom">The source room.</param>
        /// <param name="destinationRoom">The destination room.</param>
        /// <param name="direction">The direction the destinationRoom lies in relative to the sourceRoom.</param>
        /// <returns>True if the Room's connect, false if they don't connect.</returns>
        public bool TryGetDirectionOfAdjoiningRoom(Room sourceRoom, Room destinationRoom, out CardinalDirection direction)
        {
            if (sourceRoom == null || destinationRoom == null)
            {
                direction = CardinalDirection.North;
                return false;
            }

            if (sourceRoom.Column == destinationRoom.Column && sourceRoom.Row == destinationRoom.Row - 1)
            {
                direction = CardinalDirection.North;
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column && sourceRoom.Row == destinationRoom.Row + 1)
            {
                direction = CardinalDirection.South;
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column - 1 && sourceRoom.Row == destinationRoom.Row)
            {
                direction = CardinalDirection.East;
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column + 1 && sourceRoom.Row == destinationRoom.Row)
            {
                direction = CardinalDirection.West;
                return true;
            }

            direction = CardinalDirection.North;
            return false;
        }

        /// <summary>
        /// Handle examination this Region.
        /// </summary>
        /// <returns>The result of this examination.</returns>
        public override ExaminationResult Examime()
        {
            return new ExaminationResult(Name + ": " + Description.GetDescription());
        }

        /// <summary>
        /// Handle registration of all child properties of this Region that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Region.</param>
        public override void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            foreach (var r in Rooms)
            {
                children.Add(r);
                r.RegisterTransferableChildren(ref children);
            }

            base.RegisterTransferableChildren(ref children);
        }

        #endregion
    }
}