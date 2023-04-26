using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// A class for drawing maps.
    /// </summary>
    public sealed class MapDrawer : Drawer
    {
        #region Properties

        /// <summary>
        /// Get or set the string used for representing a locked exit.
        /// </summary>
        public string LockedExitString { get; set; } = "x";

        /// <summary>
        /// Get or set the string used for representing there is an item in the room.
        /// </summary>
        public string ItemInRoomString { get; set; } = "?";

        /// <summary>
        /// Get or set the string to use for vertical boundaries.
        /// </summary>
        public string VerticalBoundaryString { get; set; } = "|";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string HorizontalBoundaryString { get; set; } = "-";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string CurrentRoomString { get; set; } = "O";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string EmptyRoomString { get; set; } = "+";

        /// <summary>
        /// Get or set the type of key to use.
        /// </summary>
        public KeyType Key { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get or set the visibility mode to use for Rooms.
        /// </summary>
        public RegionDisplayMode RoomVisibilityMode { get; set; } = RegionDisplayMode.AllRegion;

        /// <summary>
        /// Get or set the detail to use for a Region map.
        /// </summary>
        public RegionMapMode RegionMapDetail { get; } = RegionMapMode.Dynamic;

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
                throw new ArgumentException("The width parameter must be greater than 0.");

            var map = string.Empty;
            var keyLines = new Queue<string>();
            var lockedExitString = $"{LockedExitString}=Locked Exit";
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
                                exitRepresentations.Add(direction, VerticalBoundaryString);
                                break;
                            case CardinalDirection.North:
                            case CardinalDirection.South:
                                exitRepresentations.Add(direction, HorizontalBoundaryString);
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
                            exitRepresentations.Add(direction, VerticalBoundaryString);
                            break;
                        case CardinalDirection.North:
                        case CardinalDirection.South:
                            exitRepresentations.Add(direction, HorizontalBoundaryString);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            map += ConstructWrappedPaddedString($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[CardinalDirection.North] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString($"{VerticalBoundaryString}     {VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString(exitRepresentations[CardinalDirection.West] + "  " + (room.Items.Any() ? ItemInRoomString : " ") + "  " + exitRepresentations[CardinalDirection.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString($"{VerticalBoundaryString}     {VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);
            map += ConstructWrappedPaddedString($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[CardinalDirection.South] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), width);

            return map;
        }

        /// <summary>
        /// Construct a undetailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="renderWidth">The render width of the map.</param>
        /// <param name="x">The region X.</param>
        /// <param name="y">The region Y.</param>
        /// <param name="width">The region width.</param>
        /// <param name="height">The region height.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string ConstructUndetailedRegionMap(Region region, int renderWidth, int x, int y, int width, int height)
        {
            // small scale map, just dots...

            var map = string.Empty;
            const string blankRoomRow = " ";
            var rooms = region.ToMatrix();

            for (var row = height - 1; row >= y; row--)
            {
                var line = string.Empty;

                for (var column = x; column < width; column++)
                {
                    var room = rooms[column, row];

                    if (room != null)
                    {
                        if (room.HasBeenVisited || RoomVisibilityMode == RegionDisplayMode.AllRegion)
                            line += region.CurrentRoom == room ? CurrentRoomString : EmptyRoomString;
                        else
                            line += blankRoomRow;
                    }
                    else
                    {
                        line += blankRoomRow;
                    }
                }

                map += ConstructWrappedPaddedString(line, renderWidth, true);
            }

            return map;
        }

        /// <summary>
        /// Construct a detailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="renderWidth">The render width of the map.</param>
        /// <param name="roomRenderWidth">The render width of the map.</param>
        /// <param name="x">The region X.</param>
        /// <param name="y">The region Y.</param>
        /// <param name="width">The region width.</param>
        /// <param name="height">The region height.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string ConstructDetailedRegionMap(Region region, int renderWidth, int roomRenderWidth, int x, int y, int width, int height)
        {
            var map = string.Empty;
            var blankRoomRow = ConstructWhitespaceString(roomRenderWidth);
            var rooms = region.ToMatrix();

            for (var row = height - 1; row >= y; row--)
            {
                for (var rowPass = 0; rowPass < 3; rowPass++)
                {
                    var line = string.Empty;

                    for (var column = x; column < width; column++)
                    {
                        var room = rooms[column, row];

                        if (room != null)
                        {
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

                    map += ConstructWrappedPaddedString(line, renderWidth, true);
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
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(CardinalDirection.North) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.North) ? " " : HorizontalBoundaryString);
                case 1:
                    return $"{(room.HasLockedExitInDirection(CardinalDirection.West) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.West) ? " " : VerticalBoundaryString)} {(isPlayerInRoom ? CurrentRoomString : " ")} {(room.HasLockedExitInDirection(CardinalDirection.East) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.East) ? " " : VerticalBoundaryString)}";
                case 2:
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(CardinalDirection.South) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.South) ? " " : HorizontalBoundaryString);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Try and get the bounds of a Region.
        /// </summary>
        /// <param name="region">The Region to check.</param>
        /// <param name="x">The x of the region.</param>
        /// <param name="y">The y of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <returns>True if the bounds could be got, else false.</returns>
        private bool TryGetRegionBounds(Region region, out int x, out int y, out int width, out int height)
        {
            if (region != null)
            {
                var matrix = region.ToMatrix();

                x = 0;
                width = matrix.GetLength(0);
                y = 0;
                height = matrix.GetLength(1);
                return true;
            }

            x = 0;
            width = 0;
            y = 0;
            height = 0;
            return false;
        }

        /// <summary>
        /// Construct a map of a Region.
        /// </summary>
        /// <param name="region">The Region to draw.</param>
        /// <param name="renderWidth">The render width.</param>
        /// <param name="renderHeight">The render height.</param>
        /// <returns>A map of the Region in a string.</returns>
        public string ConstructRegionMap(Region region, int renderWidth, int renderHeight)
        {
            if (!TryGetRegionBounds(region, out var x, out var y, out var width, out var height)) 
                return string.Empty;

            var roomWidthTotal = $"{VerticalBoundaryString}{HorizontalBoundaryString} {HorizontalBoundaryString}{VerticalBoundaryString}".Length;
            var roomHeightTotal = roomWidthTotal;
            var detailedWouldFit = (width - x) * roomWidthTotal < renderWidth - 2 && (height - y) * roomHeightTotal < renderHeight;
            var undetailedWouldFit = width - x < renderWidth - 2 && height - y < renderHeight;

            switch (RegionMapDetail)
            {
                case RegionMapMode.Detailed:
                   
                    if (detailedWouldFit)
                        return ConstructDetailedRegionMap(region, renderWidth, roomWidthTotal, x, y, width, height);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", renderWidth);

                case RegionMapMode.Dynamic:
                    
                    if (detailedWouldFit)
                        return ConstructDetailedRegionMap(region, renderWidth, roomWidthTotal, x, y, width, height);

                    if (undetailedWouldFit)
                        return ConstructUndetailedRegionMap(region, renderWidth, x, y, width, height);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", renderWidth);

                case RegionMapMode.Undetailed:
                    
                    if (undetailedWouldFit)
                        return ConstructUndetailedRegionMap(region, renderWidth, x, y, width, height);

                    return ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", renderWidth);

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}