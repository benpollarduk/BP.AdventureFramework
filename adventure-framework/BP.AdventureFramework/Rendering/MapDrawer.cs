using System;
using System.Collections.Generic;
using System.Linq;
using AdventureFramework.Locations;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// A class for drawing maps
    /// </summary>
    public class MapDrawer : Drawer
    {
        #region Propertis

        /// <summary>
        /// Get or set the string used for representing a locked exit
        /// </summary>
        public string LockedExitString
        {
            get { return lockedExitString; }
            set { lockedExitString = value; }
        }

        /// <summary>
        /// Get or set the string used for representing a locked exit
        /// </summary>
        private string lockedExitString = "x";

        /// <summary>
        /// Get or set the string used for representing there is an item in the room
        /// </summary>
        public string ItemInRoomString
        {
            get { return itemInRoomString; }
            set { itemInRoomString = value; }
        }

        /// <summary>
        /// Get or set the string used for representing there is an item in the room
        /// </summary>
        private string itemInRoomString = "?";

        /// <summary>
        /// Get or set the type of key to use
        /// </summary>
        public EKeyType Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// Get or set the type of key to use
        /// </summary>
        private EKeyType key = EKeyType.Dynamic;

        /// <summary>
        /// Get or set the visibility mode to use for Rooms
        /// </summary>
        public ERegionDisplayMode RoomVisibilityMode
        {
            get { return roomVisibilityMode; }
            set { roomVisibilityMode = value; }
        }

        /// <summary>
        /// Get or set the visibility mode to use for Rooms
        /// </summary>
        private ERegionDisplayMode roomVisibilityMode = ERegionDisplayMode.VistitedRoomsOnly;

        /// <summary>
        /// Get or set the detail to use for a Region map
        /// </summary>
        public ERegionMapMode RegionMapDetail
        {
            get { return regionMapDetail; }
            set { regionMapDetail = value; }
        }

        /// <summary>
        /// Get or set the detail to use for a Region map
        /// </summary>
        private ERegionMapMode regionMapDetail = ERegionMapMode.Dynamic;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        public MapDrawer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        public MapDrawer(EKeyType key)
        {
            // set key
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        /// <param name="lockedExitString">Specify a string used for representing a locked exit</param>
        /// <param name="itemInRoomString">Specify a string used for representing there is an item in the room</param>
        public MapDrawer(EKeyType key, string lockedExitString, string itemInRoomString)
        {
            // set key
            Key = key;

            // set string
            LockedExitString = lockedExitString;

            // set string
            ItemInRoomString = itemInRoomString;
        }

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        /// <param name="lockedExitString">Specify a string used for representing a locked exit</param>
        /// <param name="itemInRoomString">Specify a string used for representing there is an item in the room</param>
        /// <param name="roomVisibilityMode">Specify a visibility mode to be used for Room's</param>
        /// <param name="regionMapDetail">Specify a Region map detail mode</param>
        public MapDrawer(EKeyType key, string lockedExitString, string itemInRoomString, ERegionDisplayMode roomVisibilityMode, ERegionMapMode regionMapDetail)
        {
            // set key
            Key = key;

            // set string
            LockedExitString = lockedExitString;

            // set string
            ItemInRoomString = itemInRoomString;

            // set visibility mode
            RoomVisibilityMode = roomVisibilityMode;

            // set map detail
            RegionMapDetail = regionMapDetail;
        }

        /// <summary>
        /// Construct a map for a Room
        /// </summary>
        /// <param name="room">The Room to draw</param>
        /// <param name="width">The allocated with to draw within</param>
        /// <returns>A map of the Room in a String</returns>
        public virtual string ConstructRoomMap(Room room, int width)
        {
            // if width is too small
            if (width <= 0)
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");

            // hold string
            var map = string.Empty;

            // hold current line
            var mapLine = string.Empty;

            // hold lines of key
            var keyLines = new Queue<string>();

            // select key
            switch (Key)
            {
                case EKeyType.Dynamic:
                    {
                        // if locked exits
                        if (room.UnlockedExits.Where(x => x.IsPlayerVisible).Count() != room.Exits.Where(x => x.IsPlayerVisible).Count())
                            // locked exit key line
                            keyLines.Enqueue(string.Format("  {0}=Locked Exit", LockedExitString));

                        // if unlocked exits
                        if (room.UnlockedExits.Where(x => x.IsPlayerVisible).Count() > 0)
                            // unlocked exit key line
                            keyLines.Enqueue(string.Format("  {0}=Unlocked Exit", "N/E/S/W"));

                        // if an entered from
                        if (room.EnteredFrom.HasValue)
                            // entrance key line
                            keyLines.Enqueue(string.Format("  {0}=Entrance", room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)));

                        // if an item
                        if (room.Items.Where(x => x.IsPlayerVisible).Count() > 0)
                            // item key line
                            keyLines.Enqueue(string.Format("  {0}=Item(s) In Room", ItemInRoomString));

                        break;
                    }
                case EKeyType.Full:
                    {
                        // locked exit key line
                        keyLines.Enqueue(string.Format("  {0}=Locked Exit", LockedExitString));

                        // unlocked exit key line
                        keyLines.Enqueue(string.Format("  {0}=Unlocked Exit", "N/E/S/W"));

                        // entrance key line
                        keyLines.Enqueue(string.Format("  {0}=Entrance", "n/e/s/w"));

                        // item key line
                        keyLines.Enqueue(string.Format("  {0}=Item(s) In Room", ItemInRoomString));

                        break;
                    }
                case EKeyType.None:
                    {
                        // no key

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            // hold representations of all exits
            var exitRepresentations = new Dictionary<ECardinalDirection, string>();

            // create array of exits
            ECardinalDirection[] exits = { ECardinalDirection.East, ECardinalDirection.North, ECardinalDirection.South, ECardinalDirection.West };

            // set keys
            foreach (var direction in exits)
                // if entere from this direction
                if (room.EnteredFrom == direction)
                {
                    // still check
                    if (room.HasUnlockedExitInDirection(direction))
                        // set exit representation
                        exitRepresentations.Add(direction, room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1));
                    else if (room.HasLockedExitInDirection(direction))
                        // set exit representation
                        exitRepresentations.Add(direction, LockedExitString);
                    else
                        switch (direction)
                        {
                            case ECardinalDirection.East:
                            case ECardinalDirection.West:
                                {
                                    // set exit representation
                                    exitRepresentations.Add(direction, "|");

                                    break;
                                }
                            case ECardinalDirection.North:
                            case ECardinalDirection.South:
                                {
                                    // set exit representation
                                    exitRepresentations.Add(direction, "-");

                                    break;
                                }
                            default:
                                {
                                    throw new NotImplementedException();
                                }
                        }
                }
                else if (room.HasLockedExitInDirection(direction))
                {
                    // set exit representation
                    exitRepresentations.Add(direction, LockedExitString);
                }
                else if (room.HasUnlockedExitInDirection(direction))
                {
                    // set exit representation to first letter
                    exitRepresentations.Add(direction, direction.ToString().ToUpper().Substring(0, 1));
                }
                else
                {
                    // select direction
                    switch (direction)
                    {
                        case ECardinalDirection.East:
                        case ECardinalDirection.West:
                            {
                                // set exit representation
                                exitRepresentations.Add(direction, "|");

                                break;
                            }
                        case ECardinalDirection.North:
                        case ECardinalDirection.South:
                            {
                                // set exit representation
                                exitRepresentations.Add(direction, "-");

                                break;
                            }
                        default:
                            {
                                throw new NotImplementedException();
                            }
                    }
                }

            // add top row
            map += ConstructWrappedPaddedString("|--" + exitRepresentations[ECardinalDirection.North] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += ConstructWrappedPaddedString(exitRepresentations[ECardinalDirection.West] + "  " + (room.Items.Length > 0 ? "?" : " ") + "  " + exitRepresentations[ECardinalDirection.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add bottom row
            map += ConstructWrappedPaddedString("|--" + exitRepresentations[ECardinalDirection.South] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // return map
            return map;
        }

        /// <summary>
        /// Construct an undetailed Region map
        /// </summary>
        /// <param name="region">The Region to construct the map for</param>
        /// <param name="width">The width of the map</param>
        /// <param name="minColumn">The minimum column any Room within the Region</param>
        /// <param name="maxColumn">The maximum column any Room within the Region</param>
        /// <param name="minRow">The minimum row any Room within the Region</param>
        /// <param name="maxRow">The maximum row any Room within the Region</param>
        /// <returns>A reresentation of the Region as a string</returns>
        private string constructUndetailedRegionMap(Region region, int width, int minColumn, int maxColumn, int minRow, int maxRow)
        {
            // small scale map, just dots...

            // get empty string
            var map = string.Empty;

            // hold a blank room row
            var blankRoomRow = " ";

            // itterate all room rows
            for (var rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
            {
                // hold line
                var line = string.Empty;

                // itterate columns
                for (var columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                {
                    // get all matching rooms
                    var roomsWithMatchingColumnAndRow = region.Rooms.Where(r => r.Column == columnIndex && r.Row == rowIndex);

                    // if a matching room
                    if (roomsWithMatchingColumnAndRow != null &&
                        roomsWithMatchingColumnAndRow.Count() > 0)
                    {
                        // hold room
                        var rElement = roomsWithMatchingColumnAndRow.ElementAt(0);

                        // if visible
                        if (rElement.HasBeenVisited ||
                            RoomVisibilityMode == ERegionDisplayMode.AllRegion)
                            // add a new spacer for the room
                            line += region.CurrentRoom == rElement ? "O" : "+";
                        else
                            // add a new row pass
                            line += blankRoomRow;
                    }
                    else
                    {
                        // add a new row pass
                        line += blankRoomRow;
                    }
                }

                // add line
                map += ConstructWrappedPaddedString(line, width, true);
            }

            // return the map
            return map;
        }

        /// <summary>
        /// Construct a detailed Region map
        /// </summary>
        /// <param name="region">The Region to construct the map for</param>
        /// <param name="width">The width of the map</param>
        /// <param name="roomWidth">The width of each room</param>
        /// <param name="minColumn">The minimum column any Room within the Region</param>
        /// <param name="maxColumn">The maximum column any Room within the Region</param>
        /// <param name="minRow">The minimum row any Room within the Region</param>
        /// <param name="maxRow">The maximum row any Room within the Region</param>
        /// <returns>A reresentation of the Region as a string</returns>
        private string constructDetailedRegionMap(Region region, int width, int roomWidth, int minColumn, int maxColumn, int minRow, int maxRow)
        {
            // get empty string
            var map = string.Empty;

            // hold a blank room row
            var blankRoomRow = ConstructWhitespaceString(roomWidth);

            // itterate all room rows
            for (var rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
                // now do row parses
            for (var rowPass = 0; rowPass < 3; rowPass++)
            {
                // hold line
                var line = string.Empty;

                // itterate columns
                for (var columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                {
                    // get all matching rooms
                    var roomsWithMatchingColumnAndRow = region.Rooms.Where(r => r.Column == columnIndex && r.Row == rowIndex);

                    // if a matching room
                    if (roomsWithMatchingColumnAndRow != null &&
                        roomsWithMatchingColumnAndRow.Count() > 0)
                    {
                        // hold room
                        var rElement = roomsWithMatchingColumnAndRow.ElementAt(0);

                        // if been visited
                        if (rElement.HasBeenVisited ||
                            RoomVisibilityMode == ERegionDisplayMode.AllRegion)
                            // add a new spacer for the room
                            line += constructRoomRowString(rElement, region.CurrentRoom == rElement, rowPass);
                        else
                            // add a new row pass
                            line += blankRoomRow;
                    }
                    else
                    {
                        // add a new row pass
                        line += blankRoomRow;
                    }
                }

                // add line
                map += ConstructWrappedPaddedString(line, width, true);
            }

            // return the map
            return map;
        }

        /// <summary>
        /// Construct a string representing a row clice of a room
        /// </summary>
        /// <param name="room">The Room to get a slice of</param>
        /// <param name="isPlayerInRoom">If the player is in the room</param>
        /// <param name="row">The slice of the row to get (0 = top, 1 = middle, 2 = bottom)</param>
        /// <returns>The constructed room slice</returns>
        private string constructRoomRowString(Room room, bool isPlayerInRoom, int row)
        {
            // select row
            switch (row)
            {
                case 0:
                    {
                        // looking for something like |   |
                        return string.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(ECardinalDirection.North) ? LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.North) ? " " : "-");
                    }
                case 1:
                    {
                        // looking for something like |   |
                        return string.Format("{0} {1} {2}", room.HasLockedExitInDirection(ECardinalDirection.West) ? LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.West) ? " " : "|", isPlayerInRoom ? "O" : " ", room.HasLockedExitInDirection(ECardinalDirection.East) ? LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.East) ? " " : "|");
                    }
                case 2:
                    {
                        // looking for something like |   |
                        return string.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(ECardinalDirection.South) ? LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.South) ? " " : "-");
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Try and get the extremeties of a Region
        /// </summary>
        /// <param name="region">The Region to check</param>
        /// <param name="minColumn">The lowest valued column</param>
        /// <param name="maxColumn">The highest valued colummn</param>
        /// <param name="minRow">The lowest valued row</param>
        /// <param name="maxRow">The highest valued row</param>
        /// <returns>True if the check was sucsessful, else false</returns>
        private bool tryGetRegionExtremities(Region region, out int minColumn, out int maxColumn, out int minRow, out int maxRow)
        {
            // if test is applicable
            if (region != null &&
                region.Rooms.Length > 0)
            {
                // set defaults
                minColumn = region.Rooms[0].Column;
                maxColumn = region.Rooms[0].Column;
                minRow = region.Rooms[0].Row;
                maxRow = region.Rooms[0].Row;

                // itterate rooms
                foreach (GameLocation gL in region.Rooms)
                {
                    // get min column
                    minColumn = Math.Min(minColumn, gL.Column);

                    // get max column
                    maxColumn = Math.Max(maxColumn, gL.Column);

                    // get min row
                    minRow = Math.Min(minRow, gL.Row);

                    // get max row
                    maxRow = Math.Max(maxRow, gL.Row);
                }

                // pass
                return true;
            }

            // default all
            minColumn = 0;
            maxColumn = 0;
            minRow = 0;
            maxRow = 0;

            // fail
            return false;
        }

        /// <summary>
        /// Construct a map of a Region
        /// </summary>
        /// <param name="region">The Region to draw</param>
        /// <param name="width">The allocated width to draw within</param>
        /// <param name="height">The allocated height to draw within</param>
        /// <returns>A map of the Region in a String</returns>
        public virtual string ConstructRegionMap(Region region, int width, int height)
        {
            // have to try and draw within area

            // get defaults
            int minColumn;
            int minRow;
            int maxColumn;
            int maxRow;

            // if extremeties can be gotten
            if (tryGetRegionExtremities(region, out minColumn, out maxColumn, out minRow, out maxRow))
            {
                // hold room width total
                var roomWidthTotal = "|- -|".Length;

                // hold room height in total
                var roomHeightTotal = roomWidthTotal;

                // hold if detailed fits
                var detailedWouldFit = (maxColumn - minColumn) * roomWidthTotal < width - 2 && (maxRow - minRow) * roomHeightTotal < height;

                // hold if undetailed fits
                var undetailedWouldFit = maxColumn - minColumn < width - 2 && maxRow - minRow < height;

                // select detail
                switch (RegionMapDetail)
                {
                    case ERegionMapMode.Detailed:
                        {
                            // if detailed would fit
                            if (detailedWouldFit)
                                // draw the map
                                return constructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);
                            return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                        }
                    case ERegionMapMode.Dynamic:
                        {
                            // if within bounds
                            if (detailedWouldFit)
                                // draw the map
                                return constructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);
                            if (undetailedWouldFit)
                                // draw the map
                                return constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);
                            return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                        }
                    case ERegionMapMode.Undetailed:
                        {
                            // if within bounds
                            if (undetailedWouldFit)
                                // draw the map
                                return constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);
                            return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }

            // no map available
            return string.Empty;
        }

        #endregion
    }

    /// <summary>
    /// Enumeration of region map modes
    /// </summary>
    public enum ERegionMapMode
    {
        /// <summary>
        /// Shows rooms at a detailed level
        /// </summary>
        Detailed = 0,

        /// <summary>
        /// Shows rooms as one character, which allows larger maps to be displayed in a limited area
        /// </summary>
        Undetailed,

        /// <summary>
        /// Dynamic region map - uses detailed if there is room, else map will be undetailed
        /// </summary>
        Dynamic
    }

    /// <summary>
    /// Enumeration of region display modes
    /// </summary>
    public enum ERegionDisplayMode
    {
        /// <summary>
        /// Only show visited rooms
        /// </summary>
        VistitedRoomsOnly = 0,

        /// <summary>
        /// Show the whole region if it has been visited or not
        /// </summary>
        AllRegion
    }
}