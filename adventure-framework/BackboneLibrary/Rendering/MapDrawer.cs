using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Interaction;

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
        public String LockedExitString
        {
            get { return this.lockedExitString; }
            set { this.lockedExitString = value; }
        }

        /// <summary>
        /// Get or set the string used for representing a locked exit
        /// </summary>
        private String lockedExitString = "x";

        /// <summary>
        /// Get or set the string used for representing there is an item in the room
        /// </summary>
        public String ItemInRoomString
        {
            get { return this.itemInRoomString; }
            set { this.itemInRoomString = value; }
        }

        /// <summary>
        /// Get or set the string used for representing there is an item in the room
        /// </summary>
        private String itemInRoomString = "?";

        /// <summary>
        /// Get or set the type of key to use
        /// </summary>
        public EKeyType Key
        {
            get { return this.key; }
            set { this.key = value; }
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
            get { return this.roomVisibilityMode; }
            set { this.roomVisibilityMode = value; }
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
            get { return this.regionMapDetail; }
            set { this.regionMapDetail = value; }
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
            this.Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        /// <param name="lockedExitString">Specify a string used for representing a locked exit</param>
        /// <param name="itemInRoomString">Specify a string used for representing there is an item in the room</param>
        public MapDrawer(EKeyType key, String lockedExitString, String itemInRoomString)
        {
            // set key
            this.Key = key;

            // set string
            this.LockedExitString = lockedExitString;

            // set string
            this.ItemInRoomString = itemInRoomString;
        }

        /// <summary>
        /// Initializes a new instance of the MapDrawer class
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        /// <param name="lockedExitString">Specify a string used for representing a locked exit</param>
        /// <param name="itemInRoomString">Specify a string used for representing there is an item in the room</param>
        /// <param name="roomVisibilityMode">Specify a visibility mode to be used for Room's</param>
        /// <param name="regionMapDetail">Specify a Region map detail mode</param>
        public MapDrawer(EKeyType key, String lockedExitString, String itemInRoomString, ERegionDisplayMode roomVisibilityMode, ERegionMapMode regionMapDetail)
        {
            // set key
            this.Key = key;

            // set string
            this.LockedExitString = lockedExitString;

            // set string
            this.ItemInRoomString = itemInRoomString;

            // set visibility mode
            this.RoomVisibilityMode = roomVisibilityMode;

            // set map detail
            this.RegionMapDetail = regionMapDetail;
        }

        /// <summary>
        /// Construct a map for a Room
        /// </summary>
        /// <param name="room">The Room to draw</param>
        /// <param name="width">The allocated with to draw within</param>
        /// <returns>A map of the Room in a String</returns>
        public virtual String ConstructRoomMap(Room room, Int32 width)
        {
            // if width is too small
            if (width <= 0)
            {
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");
            }

            // hold string
            String map = String.Empty;

            // hold current line
            String mapLine = String.Empty;

            // hold lines of key
            Queue<String> keyLines = new Queue<String>();

            // select key
            switch (this.Key)
            {
                case (EKeyType.Dynamic):
                    {
                        // if locked exits
                        if (room.UnlockedExits.Where<Exit>((Exit x) => x.IsPlayerVisible).Count<Exit>() != room.Exits.Where<Exit>((Exit x) => x.IsPlayerVisible).Count<Exit>())
                        {
                            // locked exit key line
                            keyLines.Enqueue(String.Format("  {0}=Locked Exit", this.LockedExitString));
                        }

                        // if unlocked exits
                        if (room.UnlockedExits.Where<Exit>((Exit x) => x.IsPlayerVisible).Count<Exit>() > 0)
                        {
                            // unlocked exit key line
                            keyLines.Enqueue(String.Format("  {0}=Unlocked Exit", "N/E/S/W"));
                        }

                        // if an entered from
                        if (room.EnteredFrom.HasValue)
                        {
                            // entrance key line
                            keyLines.Enqueue(String.Format("  {0}=Entrance", room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)));
                        }

                        // if an item
                        if (room.Items.Where<Item>((Item x) => x.IsPlayerVisible).Count<Item>() > 0)
                        {
                            // item key line
                            keyLines.Enqueue(String.Format("  {0}=Item(s) In Room", this.ItemInRoomString));
                        }

                        break;
                    }
                case (EKeyType.Full):
                    {
                        // locked exit key line
                        keyLines.Enqueue(String.Format("  {0}=Locked Exit", this.LockedExitString));

                        // unlocked exit key line
                        keyLines.Enqueue(String.Format("  {0}=Unlocked Exit", "N/E/S/W"));

                        // entrance key line
                        keyLines.Enqueue(String.Format("  {0}=Entrance", "n/e/s/w"));

                        // item key line
                        keyLines.Enqueue(String.Format("  {0}=Item(s) In Room", this.ItemInRoomString));

                        break;
                    }
                case (EKeyType.None):
                    {
                        // no key

                        break;
                    }
                default: { throw new NotImplementedException(); }
            }

            // hold representations of all exits
            Dictionary<ECardinalDirection, String> exitRepresentations = new Dictionary<ECardinalDirection, String>();

            // create array of exits
            ECardinalDirection[] exits = { ECardinalDirection.East, ECardinalDirection.North, ECardinalDirection.South, ECardinalDirection.West };

            // set keys
            foreach (ECardinalDirection direction in exits)
            {
                // if entere from this direction
                if (room.EnteredFrom == direction)
                {
                    // still check
                    if (room.HasUnlockedExitInDirection(direction))
                    {
                        // set exit representation
                        exitRepresentations.Add(direction, room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1));
                    }
                    else if (room.HasLockedExitInDirection(direction))
                    {
                        // set exit representation
                        exitRepresentations.Add(direction, this.LockedExitString);
                    }
                    else
                    {
                        switch (direction)
                        {
                            case (ECardinalDirection.East):
                            case (ECardinalDirection.West):
                                {
                                    // set exit representation
                                    exitRepresentations.Add(direction, "|");

                                    break;
                                }
                            case (ECardinalDirection.North):
                            case (ECardinalDirection.South):
                                {
                                    // set exit representation
                                    exitRepresentations.Add(direction, "-");

                                    break;
                                }
                            default: { throw new NotImplementedException(); }
                        }
                    }
                }
                else if (room.HasLockedExitInDirection(direction))
                {
                    // set exit representation
                    exitRepresentations.Add(direction, this.LockedExitString);
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
                        case (ECardinalDirection.East):
                        case (ECardinalDirection.West):
                            {
                                // set exit representation
                                exitRepresentations.Add(direction, "|");

                                break;
                            }
                        case (ECardinalDirection.North):
                        case (ECardinalDirection.South):
                            {
                                // set exit representation
                                exitRepresentations.Add(direction, "-");

                                break;
                            }
                        default: { throw new NotImplementedException(); }
                    }
                }
            }

            // add top row
            map += this.ConstructWrappedPaddedString("|--" + exitRepresentations[ECardinalDirection.North] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += this.ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += this.ConstructWrappedPaddedString(exitRepresentations[ECardinalDirection.West] + "  " + (room.Items.Length > 0 ? "?" : " ") + "  " + exitRepresentations[ECardinalDirection.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add middle row
            map += this.ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            // add bottom row
            map += this.ConstructWrappedPaddedString("|--" + exitRepresentations[ECardinalDirection.South] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

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
        private String constructUndetailedRegionMap(Region region, Int32 width, Int32 minColumn, Int32 maxColumn, Int32 minRow, Int32 maxRow)
        {
            // small scale map, just dots...

            // get empty string
            String map = String.Empty;

            // hold a blank room row
            String blankRoomRow = " ";

            // itterate all room rows
            for (Int32 rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
            {
                // hold line
                String line = String.Empty;

                // itterate columns
                for (Int32 columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                {
                    // get all matching rooms
                    IEnumerable<Room> roomsWithMatchingColumnAndRow = region.Rooms.Where<Room>((Room r) => ((r.Column == columnIndex) && (r.Row == rowIndex)));

                    // if a matching room
                    if ((roomsWithMatchingColumnAndRow != null) &&
                        (roomsWithMatchingColumnAndRow.Count<Room>() > 0))
                    {
                        // hold room
                        Room rElement = roomsWithMatchingColumnAndRow.ElementAt<Room>(0);

                        // if visible
                        if ((rElement.HasBeenVisited) ||
                            (this.RoomVisibilityMode == ERegionDisplayMode.AllRegion))
                        {
                            // add a new spacer for the room
                            line += region.CurrentRoom == rElement ? "O" : "+";
                        }
                        else
                        {
                            // add a new row pass
                            line += blankRoomRow;
                        }
                    }
                    else
                    {
                        // add a new row pass
                        line += blankRoomRow;
                    }
                }

                // add line
                map += this.ConstructWrappedPaddedString(line, width, true);
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
        private String constructDetailedRegionMap(Region region, Int32 width, Int32 roomWidth, Int32 minColumn, Int32 maxColumn, Int32 minRow, Int32 maxRow)
        {
            // get empty string
            String map = String.Empty;

            // hold a blank room row
            String blankRoomRow = this.ConstructWhitespaceString(roomWidth);

            // itterate all room rows
            for (Int32 rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
            {
                // now do row parses
                for (Int32 rowPass = 0; rowPass < 3; rowPass++)
                {
                    // hold line
                    String line = String.Empty;

                    // itterate columns
                    for (Int32 columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                    {
                        // get all matching rooms
                        IEnumerable<Room> roomsWithMatchingColumnAndRow = region.Rooms.Where<Room>((Room r) => ((r.Column == columnIndex) && (r.Row == rowIndex)));

                        // if a matching room
                        if ((roomsWithMatchingColumnAndRow != null) &&
                            (roomsWithMatchingColumnAndRow.Count<Room>() > 0))
                        {
                            // hold room
                            Room rElement = roomsWithMatchingColumnAndRow.ElementAt<Room>(0);

                            // if been visited
                            if ((rElement.HasBeenVisited) ||
                                (this.RoomVisibilityMode == ERegionDisplayMode.AllRegion))
                            {
                                // add a new spacer for the room
                                line += this.constructRoomRowString(rElement, region.CurrentRoom == rElement, rowPass);
                            }
                            else
                            {
                                // add a new row pass
                                line += blankRoomRow;
                            }
                        }
                        else
                        {
                            // add a new row pass
                            line += blankRoomRow;
                        }
                    }

                    // add line
                    map += this.ConstructWrappedPaddedString(line, width, true);
                }
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
        private String constructRoomRowString(Room room, Boolean isPlayerInRoom, Int32 row)
        {
            // select row
            switch (row)
            {
                case (0):
                    {
                        // looking for something like |   |
                        return String.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(ECardinalDirection.North) ? this.LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.North) ? " " : "-");
                    }
                case (1):
                    {
                        // looking for something like |   |
                        return String.Format("{0} {1} {2}", room.HasLockedExitInDirection(ECardinalDirection.West) ? this.LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.West) ? " " : "|", isPlayerInRoom ? "O" : " ", room.HasLockedExitInDirection(ECardinalDirection.East) ? this.LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.East) ? " " : "|");
                    }
                case (2):
                    {
                        // looking for something like |   |
                        return String.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(ECardinalDirection.South) ? this.LockedExitString : room.HasUnlockedExitInDirection(ECardinalDirection.South) ? " " : "-");
                    }
                default: { throw new NotImplementedException(); }
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
        private Boolean tryGetRegionExtremities(Region region, out Int32 minColumn, out Int32 maxColumn, out Int32 minRow, out Int32 maxRow)
        {
            // if test is applicable
            if ((region != null) &&
                (region.Rooms.Length > 0))
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
            else
            {
                // default all
                minColumn = 0;
                maxColumn = 0;
                minRow = 0;
                maxRow = 0;

                // fail
                return false;
            }
        }

        /// <summary>
        /// Construct a map of a Region
        /// </summary>
        /// <param name="region">The Region to draw</param>
        /// <param name="width">The allocated width to draw within</param>
        /// <param name="height">The allocated height to draw within</param>
        /// <returns>A map of the Region in a String</returns>
        public virtual String ConstructRegionMap(Region region, Int32 width, Int32 height)
        {
            // have to try and draw within area
            
            // get defaults
            Int32 minColumn;
            Int32 minRow;
            Int32 maxColumn;
            Int32 maxRow;

            // if extremeties can be gotten
            if (this.tryGetRegionExtremities(region, out minColumn, out maxColumn, out minRow, out maxRow))
            {
                // hold room width total
                Int32 roomWidthTotal = "|- -|".Length;

                // hold room height in total
                Int32 roomHeightTotal = roomWidthTotal;

                // hold if detailed fits
                Boolean detailedWouldFit = ((maxColumn - minColumn) * roomWidthTotal < width - 2) && ((maxRow - minRow) * roomHeightTotal < height);

                // hold if undetailed fits
                Boolean undetailedWouldFit = ((maxColumn - minColumn) < width - 2) && ((maxRow - minRow) < height);

                // select detail
                switch (this.RegionMapDetail)
                {
                    case (ERegionMapMode.Detailed):
                        {
                            // if detailed would fit
                            if (detailedWouldFit)
                            {
                                // draw the map
                                return this.constructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);
                            }
                            else
                            {
                                // return error string
                                return this.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                            }
                        }
                    case (ERegionMapMode.Dynamic):
                        {
                            // if within bounds
                            if (detailedWouldFit)
                            {
                                // draw the map
                                return this.constructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);
                            }
                            else if (undetailedWouldFit)
                            {
                                // draw the map
                                return this.constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);
                            }
                            else
                            {
                                // return error string
                                return this.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                            }
                        }
                    case (ERegionMapMode.Undetailed):
                        {
                            // if within bounds
                            if (undetailedWouldFit)
                            {
                                // draw the map
                                return this.constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);
                            }
                            else
                            {
                                // return error string
                                return this.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);
                            }
                        }
                    default: { throw new NotImplementedException(); }
                }
            }
            else
            {
                // no map available
                return string.Empty;
            }
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
