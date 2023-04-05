using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BP.AdventureFramework.Interaction;

namespace AdventureFramework.Locations
{
    /// <summary>
    /// Represents a region
    /// </summary>
    public class Region : GameLocation
    {
        #region Properties

        /// <summary>
        /// Get the Rooms in this Region
        /// </summary>
        public Room[] Rooms
        {
            get { return rooms.ToArray<Room>(); }
            protected set { rooms = new List<Room>(value); }
        }

        /// <summary>
        /// Get or set the Rooms in this Region
        /// </summary>
        private List<Room> rooms = new List<Room>();

        /// <summary>
        /// Get the current Room
        /// </summary>
        public Room CurrentRoom
        {
            get
            {
                // if no current room
                if (currentRoom == null)
                    // if atleast a room
                    if (Rooms.Count() > 0)
                        // set start room
                        SetStartRoom(0);

                // return current
                return currentRoom;
            }
            protected set { currentRoom = value; }
        }

        /// <summary>
        /// Get or set the current Room
        /// </summary>
        private Room currentRoom;

        /// <summary>
        /// Get the first Room found at a specified location
        /// </summary>
        /// <param name="column">The column of the Room</param>
        /// <param name="row">The row of the Room</param>
        /// <returns>The adjoining room</returns>
        public Room this[int column, int row]
        {
            get
            {
                // get rooms
                var roomsInLocation = Rooms.Where(r => r.Column == column && r.Row == row).ToArray();

                // if some rooms
                if (roomsInLocation != null &&
                    roomsInLocation.Length > 0)
                    // return first room in specified position
                    return roomsInLocation[0];
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Region class
        /// </summary>
        protected Region()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Region class
        /// </summary>
        /// <param name="name">The name of this Region</param>
        /// <param name="description">The description of this Region</param>
        public Region(string name, string description)
        {
            // set name
            this.name = name;

            // set description
            this.Description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the Region class
        /// </summary>
        /// <param name="name">The name of this Region</param>
        /// <param name="description">The description of this Region</param>
        public Region(string name, Description description)
        {
            // set name
            this.name = name;

            // set description
            this.Description = description;
        }

        /// <summary>
        /// Create a Room in this Region
        /// </summary>
        /// <param name="room">The Room to create</param>
        /// <param name="columnInRegion">The column of the Room with this Region</param>
        /// <param name="rowInRegion">The row of the Room within this Region</param>
        public bool CreateRoom(Room room, int columnInRegion, int rowInRegion)
        {
            // hold if ok to add
            var isOkToAdd = true;

            // set column
            room.Column = columnInRegion;

            // set row
            room.Row = rowInRegion;

            // itterate rooms
            foreach (var r in Rooms)
                // if collided
                if (r.Column == room.Column &&
                    r.Row == room.Row)
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }

            // if ok to add
            if (isOkToAdd)
                // add room
                rooms.Add(room);

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Create a Room in this Region
        /// </summary>
        /// <param name="room">The Room to create</param>
        /// <param name="relativeLocation">The direction this Room lies in relative to the last Room created</param>
        public bool CreateRoom(Room room, ECardinalDirection relativeLocation)
        {
            // hold if ok to add
            var isOkToAdd = true;

            // set column
            room.Column = Rooms.Length > 0 ? Rooms[Rooms.Length - 1].Column : 0;

            // set row
            room.Row = Rooms.Length > 0 ? Rooms[Rooms.Length - 1].Row : 0;

            // if a previously added room
            if (Rooms.Length > 0)
                // select direction
                switch (relativeLocation)
                {
                    case ECardinalDirection.East:
                        {
                            // set column
                            room.Column++;

                            break;
                        }
                    case ECardinalDirection.North:
                        {
                            // set row
                            room.Row++;

                            break;
                        }
                    case ECardinalDirection.South:
                        {
                            // set row
                            room.Row--;

                            break;
                        }
                    case ECardinalDirection.West:
                        {
                            // set column
                            room.Column--;

                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }

            // itterate rooms
            foreach (var r in Rooms)
                // if collided
                if (r.Column == room.Column &&
                    r.Row == room.Row)
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }

            // if ok to add
            if (isOkToAdd)
                // add room
                rooms.Add(room);

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Get an adjoining Room to the Region.CurrentRoom property
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room</param>
        /// <returns>The adjoining Room, if there is one</returns>
        public virtual Room GetAdjoiningRoom(ECardinalDirection direction)
        {
            // get adjoining room
            return GetAdjoiningRoom(direction, CurrentRoom);
        }

        /// <summary>
        /// Get an adjoining Room to a specified Room
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room</param>
        /// <param name="startRoom">The Room to start the check in</param>
        /// <returns>The adjoining Room, if there is one</returns>
        public virtual Room GetAdjoiningRoom(ECardinalDirection direction, Room startRoom)
        {
            // if a move is valid
            if (startRoom.CanMove(direction))
            {
                // hold column to look for
                int columnToLookFor;

                // hold row to look for
                int rowToLookFor;

                // select direction
                switch (direction)
                {
                    case ECardinalDirection.East:
                        {
                            // set column
                            columnToLookFor = startRoom.Column + 1;

                            // set row
                            rowToLookFor = startRoom.Row;

                            break;
                        }
                    case ECardinalDirection.North:
                        {
                            // set column
                            columnToLookFor = startRoom.Column;

                            // set row
                            rowToLookFor = startRoom.Row + 1;

                            break;
                        }
                    case ECardinalDirection.South:
                        {
                            // set column
                            columnToLookFor = startRoom.Column;

                            // set row
                            rowToLookFor = startRoom.Row - 1;

                            break;
                        }
                    case ECardinalDirection.West:
                        {
                            // set column
                            columnToLookFor = startRoom.Column - 1;

                            // set row
                            rowToLookFor = startRoom.Row;

                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }

                // find room
                return this[columnToLookFor, rowToLookFor];
            }

            // no room
            return null;
        }

        /// <summary>
        /// Move in a specified direction
        /// </summary>
        /// <param name="direction">The direction to move in</param>
        /// <returns>If a move was sucsessful</returns>
        public bool Move(ECardinalDirection direction)
        {
            // move room
            if (CurrentRoom.CanMove(direction))
            {
                // move to the current room
                CurrentRoom = GetAdjoiningRoom(direction);

                // handle moving into
                CurrentRoom.OnMovedInto((ECardinalDirection)(-(int)direction));

                // pass
                return true;
            }

            // fail
            return false;
        }

        /// <summary>
        /// Move to a specified Room
        /// </summary>
        /// <param name="roomName">The name of the Room to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public bool Move(string roomName)
        {
            // get matches of name
            var matches = Rooms.Where(r => r.Name == roomName);

            // move room
            if (matches.Count() > 0)
                // move room
                return Move(matches.ElementAt(0));
            return false;
        }

        /// <summary>
        /// Move to a specified room
        /// </summary>
        /// <param name="room">The room to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public bool Move(Room room)
        {
            // move room
            if (Rooms.Contains(room))
            {
                // hold direction
                ECardinalDirection direction;

                // if direction is found
                var movedCorrectly = TryGetDirectionOfAdjoiningRoom(CurrentRoom, room, out direction);

                // move to a room
                CurrentRoom = room;

                // if moved fine
                if (movedCorrectly)
                    // move into
                    CurrentRoom.OnMovedInto((ECardinalDirection)(-(int)direction));
                else
                    // move into
                    CurrentRoom.OnMovedInto(null);

                // pass
                return true;
            }

            // fail
            return false;
        }

        /// <summary>
        /// Set the Room to start in
        /// </summary>
        /// <param name="room">The Room to start in</param>
        public void SetStartRoom(Room room)
        {
            // set room
            CurrentRoom = room;

            // enter
            CurrentRoom.OnMovedInto(null);
        }

        /// <summary>
        /// Set the Room to start in
        /// </summary>
        /// <param name="index">The index of Room to start in</param>
        public void SetStartRoom(int index)
        {
            // set room
            SetStartRoom(Rooms[index]);
        }

        /// <summary>
        /// Unlock a pair of doors in a speciifed direction in the CurrentRoom
        /// </summary>
        /// <param name="direction">The direction to unlock in</param>
        public void UnlockDoorPair(ECardinalDirection direction)
        {
            // get exit in this room
            var exitInThisRoom = CurrentRoom[direction];

            // if exit as found
            if (exitInThisRoom != null)
            {
                // hold exit in opposing room
                Exit exitInOpposingRoom;

                // select direction
                switch (direction)
                {
                    case ECardinalDirection.East:
                        {
                            // get opposing exit
                            exitInOpposingRoom = this[CurrentRoom.Column + 1, CurrentRoom.Row][(ECardinalDirection)(-(int)direction)];

                            break;
                        }
                    case ECardinalDirection.North:
                        {
                            // get opposing exit
                            exitInOpposingRoom = this[CurrentRoom.Column, CurrentRoom.Row + 1][(ECardinalDirection)(-(int)direction)];

                            break;
                        }
                    case ECardinalDirection.South:
                        {
                            // get opposing exit
                            exitInOpposingRoom = this[CurrentRoom.Column, CurrentRoom.Row - 1][(ECardinalDirection)(-(int)direction)];

                            break;
                        }
                    case ECardinalDirection.West:
                        {
                            // get opposing exit
                            exitInOpposingRoom = this[CurrentRoom.Column - 1, CurrentRoom.Row][(ECardinalDirection)(-(int)direction)];

                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }

                // if opposing exit found
                if (exitInOpposingRoom != null)
                {
                    // unlock exit
                    exitInOpposingRoom.Unlock();

                    // unlock exit
                    exitInThisRoom.Unlock();
                }
                else
                {
                    // throw exception
                    throw new Exception("There was no opposing exit");
                }
            }
            else
            {
                // throw exception
                throw new Exception("There exit in the current room in the specified direction");
            }
        }

        /// <summary>
        /// Try and get the direction of an adjoining room
        /// </summary>
        /// <param name="sourceRoom">The source room</param>
        /// <param name="destinationRoom">The destination room</param>
        /// <param name="direction">The direction the destinationRoom lies in relative to the sourceRoom</param>
        /// <returns>True if the Room's connect, false if they don't connect</returns>
        public bool TryGetDirectionOfAdjoiningRoom(Room sourceRoom, Room destinationRoom, out ECardinalDirection direction)
        {
            // if either room is null
            if (sourceRoom == null || destinationRoom == null)
            {
                // set default
                direction = ECardinalDirection.North;

                // fail
                return false;
            }

            // check all directions
            if (sourceRoom.Column == destinationRoom.Column && sourceRoom.Row == destinationRoom.Row - 1)
            {
                // north
                direction = ECardinalDirection.North;

                // pass
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column && sourceRoom.Row == destinationRoom.Row + 1)
            {
                // south
                direction = ECardinalDirection.South;

                // pass
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column - 1 && sourceRoom.Row == destinationRoom.Row)
            {
                // east
                direction = ECardinalDirection.East;

                // pass
                return true;
            }

            if (sourceRoom.Column == destinationRoom.Column + 1 && sourceRoom.Row == destinationRoom.Row)
            {
                // west
                direction = ECardinalDirection.West;

                // pass
                return true;
            }

            // default
            direction = ECardinalDirection.North;

            // fail
            return false;
        }

        /// <summary>
        /// Handle examination this Region
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected override ExaminationResult OnExamined()
        {
            return new ExaminationResult(Name + ": " + Description.GetDescription());
        }

        /// <summary>
        /// Handle registration of all child properties of this Region that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Region</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // itterate all rooms
            foreach (var r in Rooms)
            {
                // add
                children.Add(r);

                // register children
                r.RegisterTransferableChildren(ref children);
            }

            base.OnRegisterTransferableChildren(ref children);
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Region
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Region");

            // write current room
            writer.WriteAttributeString("CurrentRoom", CurrentRoom.Name);

            // write start of rooms
            writer.WriteStartElement("Rooms");

            // itterate rooms
            for (var index = 0; index < Rooms.Length; index++)
                // write room
                Rooms[index].WriteXml(writer);

            // write end element
            writer.WriteEndElement();

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Region
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // read rooms
            var roomsNode = GetNode(node, "Rooms");

            // itterate rooms
            for (var index = 0; index < roomsNode.ChildNodes.Count; index++)
            {
                // get room node
                var roomElementNode = roomsNode.ChildNodes[index];

                // setup room
                this.rooms[index].ReadXmlNode(roomElementNode);
            }

            // get current room
            var rooms = this.rooms.Where(r => r.Name == GetAttribute(node, "CurrentRoom").Value).ToArray();

            // set current to first match
            CurrentRoom = rooms[0];

            // read base
            base.OnReadXmlNode(GetNode(node, "GameLocation"));
        }

        #endregion

        #endregion
    }
}