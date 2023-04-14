using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// A class for drawing maps.
    /// </summary>
    public class MapDrawer : Drawer
    {
        #region Properties

        /// <summary>
        /// Get or set the string used for representing a locked exit.
        /// </summary>
        public string LockedExitString { get; set; }

        /// <summary>
        /// Get or set the string used for representing there is an item in the room.
        /// </summary>
        public string ItemInRoomString { get; set; }

        /// <summary>
        /// Get or set the type of key to use.
        /// </summary>
        public KeyType Key { get; set; }

        /// <summary>
        /// Get the visibility mode to use for Rooms.
        /// </summary>
        public RegionDisplayMode RoomVisibilityMode { get; }

        /// <summary>
        /// Get the detail to use for a Region map.
        /// </summary>
        public RegionMapMode RegionMapDetail { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MapDrawer class.
        /// </summary>
        /// <param name="key">Specify the type of key to use</param>
        /// <param name="lockedExitString">Specify a string used for representing a locked exit.</param>
        /// <param name="itemInRoomString">Specify a string used for representing there is an item in the room.</param>
        /// <param name="roomVisibilityMode">Specify a visibility mode to be used for Rooms.</param>
        /// <param name="regionMapDetail">Specify a Region map detail mode.</param>
        public MapDrawer(KeyType key = KeyType.Dynamic, string lockedExitString = "x", string itemInRoomString = "?", RegionDisplayMode roomVisibilityMode = RegionDisplayMode.VistitedRoomsOnly, RegionMapMode regionMapDetail = RegionMapMode.Dynamic)
        {
            Key = key;
            LockedExitString = lockedExitString;
            ItemInRoomString = itemInRoomString;
            RoomVisibilityMode = roomVisibilityMode;
            RegionMapDetail = regionMapDetail;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Construct a map for a Room.
        /// </summary>
        /// <param name="room">The Room to draw.</param>
        /// <param name="width">The allocated with to draw within.</param>
        /// <returns>A map of the Room in a String.</returns>
        public string ConstructRoomMap(Room room, int width)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0");

            var map = string.Empty;
            var keyLines = new Queue<string>();
            var lockedExitString = "{LockedExitString}=Locked Exit";
            var unlockedExitString = "N/E/S/W = Unlocked Exit";
            var entranceString = "n/e/s/w = Entrance";
            var itemsString = $"{ItemInRoomString}=Item(s) In Room";

            switch (Key)
            {
                case KeyType.Dynamic:

                    if (room.UnlockedExits.Count(x => x.IsPlayerVisible) != room.Exits.Count(x => x.IsPlayerVisible))
                        keyLines.Enqueue($"  {lockedExitString}");

                    if (room.UnlockedExits.Any(x => x.IsPlayerVisible))
                        keyLines.Enqueue($"  {unlockedExitString}");

                    if (room.EnteredFrom.HasValue)
                        keyLines.Enqueue($"  {room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)}=Entrance");

                    if (room.Items.Any(x => x.IsPlayerVisible))
                        keyLines.Enqueue($"  {itemsString}");

                    break;

                case KeyType.Full:

                    keyLines.Enqueue($"  {lockedExitString}");
                    keyLines.Enqueue($"  {unlockedExitString}");
                    keyLines.Enqueue($"  {entranceString}");
                    keyLines.Enqueue($"  {itemsString}");

                    break;

                case KeyType.None:
                    break;
                default:
                    throw new NotImplementedException();
            }

            var exitRepresentations = new Dictionary<CardinalDirection, string>();
            CardinalDirection[] exits = { CardinalDirection.East, CardinalDirection.North, CardinalDirection.South, CardinalDirection.West };

            foreach (var direction in exits)
            {
                if (room.EnteredFrom == direction)
                {
                    if (room.HasUnlockedExitInDirection(direction))
                    {
                        if (room.EnteredFrom != null) 
                            exitRepresentations.Add(direction, room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1));
                    }
                    else if (room.HasLockedExitInDirection(direction))
                    {
                        exitRepresentations.Add(direction, LockedExitString);
                    }
                    else
                    {
                        switch (direction)
                        {
                            case CardinalDirection.East:
                            case CardinalDirection.West:
                                exitRepresentations.Add(direction, "|");
                                break;
                            case CardinalDirection.North:
                            case CardinalDirection.South:
                                exitRepresentations.Add(direction, "-");
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                        
                }
                else if (room.HasLockedExitInDirection(direction))
                {
                    exitRepresentations.Add(direction, LockedExitString);
                }
                else if (room.HasUnlockedExitInDirection(direction))
                {
                    exitRepresentations.Add(direction, direction.ToString().ToUpper().Substring(0, 1));
                }
                else
                {
                    switch (direction)
                    {
                        case CardinalDirection.East:
                        case CardinalDirection.West:
                            exitRepresentations.Add(direction, "|");
                            break;
                        case CardinalDirection.North:
                        case CardinalDirection.South:
                            exitRepresentations.Add(direction, "-");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            map += ConstructWrappedPaddedString("|--" + exitRepresentations[CardinalDirection.North] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString(exitRepresentations[CardinalDirection.West] + "  " + (room.Items.Any() ? "?" : " ") + "  " + exitRepresentations[CardinalDirection.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString("|     |" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString("|--" + exitRepresentations[CardinalDirection.South] + "--|" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            return map;
        }

        /// <summary>
        /// Construct a undetailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="width">The width of the map.</param>
        /// <param name="minColumn">The minimum column any Room within the Region.</param>
        /// <param name="maxColumn">The maximum column any Room within the Region.</param>
        /// <param name="minRow">The minimum row any Room within the Region.</param>
        /// <param name="maxRow">The maximum row any Room within the Region.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string constructUndetailedRegionMap(Region region, int width, int minColumn, int maxColumn, int minRow, int maxRow)
        {
            // small scale map, just dots...

            var map = string.Empty;
            var blankRoomRow = " ";

            for (var rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
            {
                var line = string.Empty;

                for (var columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                {
                    var roomsWithMatchingColumnAndRow = region.Rooms.Where(r => r.Column == columnIndex && r.Row == rowIndex).ToArray();

                    if (roomsWithMatchingColumnAndRow.Any())
                    {
                        var room = roomsWithMatchingColumnAndRow.ElementAt(0);

                        if (room.HasBeenVisited || RoomVisibilityMode == RegionDisplayMode.AllRegion)
                            line += region.CurrentRoom == room ? "O" : "+";
                        else
                            line += blankRoomRow;
                    }
                    else
                    {
                        line += blankRoomRow;
                    }
                }

                map += ConstructWrappedPaddedString(line, width, true);
            }

            return map;
        }

        /// <summary>
        /// Construct a detailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="width">The width of the map.</param>
        /// <param name="roomWidth">The width of each room.</param>
        /// <param name="minColumn">The minimum column any Room within the Region.</param>
        /// <param name="maxColumn">The maximum column any Room within the Region.</param>
        /// <param name="minRow">The minimum row any Room within the Region.</param>
        /// <param name="maxRow">The maximum row any Room within the Region.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string ConstructDetailedRegionMap(Region region, int width, int roomWidth, int minColumn, int maxColumn, int minRow, int maxRow)
        {
            var map = string.Empty;
            var blankRoomRow = ConstructWhitespaceString(roomWidth);

            for (var rowIndex = maxRow; rowIndex >= minRow; rowIndex--)
            {
                for (var rowPass = 0; rowPass < 3; rowPass++)
                {
                    var line = string.Empty;

                    for (var columnIndex = minColumn; columnIndex <= maxColumn; columnIndex++)
                    {
                        var roomsWithMatchingColumnAndRow = region.Rooms.Where(r => r.Column == columnIndex && r.Row == rowIndex).ToArray();

                        if (roomsWithMatchingColumnAndRow.Any())
                        {
                            var room = roomsWithMatchingColumnAndRow.ElementAt(0);

                            if (room.HasBeenVisited || RoomVisibilityMode == RegionDisplayMode.AllRegion)
                                line += ConstructRoomRowString(room, region.CurrentRoom == room, rowPass);
                            else
                                line += blankRoomRow;
                        }
                        else
                        {
                            line += blankRoomRow;
                        }
                    }

                    map += ConstructWrappedPaddedString(line, width, true);
                }
            }   
            
            return map;
        }

        /// <summary>
        /// Construct a string representing a row slice of a room.
        /// </summary>
        /// <param name="room">The Room to get a slice of.</param>
        /// <param name="isPlayerInRoom">If the player is in the room.</param>
        /// <param name="row">The slice of the row to get (0 = top, 1 = middle, 2 = bottom).</param>
        /// <returns>The constructed room slice.</returns>
        private string ConstructRoomRowString(Room room, bool isPlayerInRoom, int row)
        {
            switch (row)
            {
                case 0:
                    return string.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(CardinalDirection.North) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.North) ? " " : "-");
                case 1:
                    return $"{(room.HasLockedExitInDirection(CardinalDirection.West) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.West) ? " " : "|")} {(isPlayerInRoom ? "O" : " ")} {(room.HasLockedExitInDirection(CardinalDirection.East) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.East) ? " " : "|")}";
                case 2:
                    return string.Format("|{0}{0}{0}|", room.HasLockedExitInDirection(CardinalDirection.South) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.South) ? " " : "-");
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Try and get the extremities of a Region.
        /// </summary>
        /// <param name="region">The Region to check.</param>
        /// <param name="minColumn">The lowest valued column.</param>
        /// <param name="maxColumn">The highest valued column.</param>
        /// <param name="minRow">The lowest valued row.</param>
        /// <param name="maxRow">The highest valued row.</param>
        /// <returns>True if the check was successful, else false.</returns>
        private bool TryGetRegionExtremities(Region region, out int minColumn, out int maxColumn, out int minRow, out int maxRow)
        {
            if (region != null && region.Rooms.Any())
            {
                minColumn = region.Rooms[0].Column;
                maxColumn = region.Rooms[0].Column;
                minRow = region.Rooms[0].Row;
                maxRow = region.Rooms[0].Row;

                foreach (var gL in region.Rooms)
                {
                    minColumn = Math.Min(minColumn, gL.Column);
                    maxColumn = Math.Max(maxColumn, gL.Column);
                    minRow = Math.Min(minRow, gL.Row);
                    maxRow = Math.Max(maxRow, gL.Row);
                }

                return true;
            }

            minColumn = 0;
            maxColumn = 0;
            minRow = 0;
            maxRow = 0;
            return false;
        }

        /// <summary>
        /// Construct a map of a Region.
        /// </summary>
        /// <param name="region">The Region to draw.</param>
        /// <param name="width">The allocated width to draw within.</param>
        /// <param name="height">The allocated height to draw within.</param>
        /// <returns>A map of the Region in a string.</returns>
        public string ConstructRegionMap(Region region, int width, int height)
        {
            if (!TryGetRegionExtremities(region, out var minColumn, out var maxColumn, out var minRow, out var maxRow)) 
                return string.Empty;

            var roomWidthTotal = "|- -|".Length;
            var roomHeightTotal = roomWidthTotal;
            var detailedWouldFit = (maxColumn - minColumn) * roomWidthTotal < width - 2 && (maxRow - minRow) * roomHeightTotal < height;
            var undetailedWouldFit = maxColumn - minColumn < width - 2 && maxRow - minRow < height;

            switch (RegionMapDetail)
            {
                case RegionMapMode.Detailed:
                   
                    if (detailedWouldFit)
                        return ConstructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);

                case RegionMapMode.Dynamic:
                    
                    if (detailedWouldFit)
                        return ConstructDetailedRegionMap(region, width, roomWidthTotal, minColumn, maxColumn, minRow, maxRow);

                    if (undetailedWouldFit)
                        return constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);

                case RegionMapMode.Undetailed:
                    
                    if (undetailedWouldFit)
                        return constructUndetailedRegionMap(region, width, minColumn, maxColumn, minRow, maxRow);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area", width);

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}