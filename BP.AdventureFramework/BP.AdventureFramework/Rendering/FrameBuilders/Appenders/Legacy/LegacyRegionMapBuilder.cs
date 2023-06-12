using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a legacy builder for region maps.
    /// </summary>
    public class LegacyRegionMapBuilder : IRegionMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the string used for representing a locked exit.
        /// </summary>
        public string LockedExitString { get; set; } = "x";

        /// <summary>
        /// Get or set the string to use for vertical boundaries.
        /// </summary>
        public string VerticalBoundaryString { get; set; } = "|";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string HorizontalBoundaryString { get; set; } = "-";

        /// <summary>
        /// Get or set the string to use for indicating the current room.
        /// </summary>
        public string CurrentRoomString { get; set; } = "O";

        /// <summary>
        /// Get or set the string to use for empty rooms.
        /// </summary>
        public string EmptyRoomString { get; set; } = "+";

        /// <summary>
        /// Get or set the string to use for the current floor.
        /// </summary>
        public string CurrentFloorIndicatorString { get; set; } = "*";

        /// <summary>
        /// Get or set the detail to use for a Region map.
        /// </summary>
        public RegionMapMode RegionMapDetail { get; } = RegionMapMode.Dynamic;

        #endregion

        #region Methods

        /// <summary>
        /// Build floor indicators.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="totalFloors">The total floor.</param>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <returns>A list of strings representing the floor indicators.</returns>
        private List<string> BuildFloorIndicators(Region region, int totalFloors, LineStringBuilder lineStringBuilder)
        {
            var floorIndicators = new List<string>();
            var matrix = region.ToMatrix();
            var currentRoom = region.GetPositionOfRoom(region.CurrentRoom);
            var currentFloor = currentRoom.Z;
            var rooms = matrix.ToRooms().Where(r => r != null).ToArray();

            for (var l = totalFloors - 1; l >= 0; l--)
            {
                var roomsOnThisFloor = rooms.Where(r => region.GetPositionOfRoom(r).Z == l).ToArray();

                // only draw levels indicators where a region is visible without discovery or a room on the floor has been visited
                if (!region.VisibleWithoutDiscovery && roomsOnThisFloor.All(r => !r.HasBeenVisited))
                    continue;

                floorIndicators.Add(BuildFloorIndicator(l, l == currentFloor, lineStringBuilder));
            }

            return floorIndicators;
        }

        /// <summary>
        /// Build a floor indicator.
        /// </summary>
        /// <param name="floor">The floor.</param>
        /// <param name="isCurrent">True if this is the current floor, else false.</param>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <returns>A string representing the floor indicator.</returns>
        private string BuildFloorIndicator(int floor, bool isCurrent, LineStringBuilder lineStringBuilder)
        {
            var floorIndicatorWhitespace = lineStringBuilder.BuildWhitespace(CurrentFloorIndicatorString.Length);
            return $"{(isCurrent ? CurrentFloorIndicatorString : floorIndicatorWhitespace)} L{floor}    ";
        }

        /// <summary>
        /// Build an undetailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="firstRoomX">The X location within the region, in rooms, to begin building at.</param>
        /// <param name="firstRoomY">The Y location within the region, in rooms, to begin building at.</param>
        /// <param name="roomsToBuildX">The number of rooms to build in the X direction.</param>
        /// <param name="roomsToBuildY">The number of rooms to build in the Y direction.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string BuildUndetailedRegionMap(Region region, LineStringBuilder lineStringBuilder, int availableColumns, int firstRoomX, int firstRoomY, int roomsToBuildX, int roomsToBuildY)
        {
            // small scale map, just dots...

            var map = string.Empty;
            const string blankRoomRow = " ";
            var rooms = region.ToMatrix();
            var currentRoomPosition = region.GetPositionOfRoom(region.CurrentRoom);
            var z = currentRoomPosition?.Z ?? 0;
            var floors = rooms.Depth;
            var floorIndicators = BuildFloorIndicators(region, floors, lineStringBuilder);
            var longestIndicatorAsWhiteSpace = lineStringBuilder.BuildWhitespace(floorIndicators.Max(x => x.Length));

            for (var y = roomsToBuildY - 1; y >= firstRoomY; y--)
            {
                var line = string.Empty;

                for (var x = firstRoomX; x < roomsToBuildX; x++)
                {
                    if (floorIndicators.Any())
                    {
                        line += floorIndicators[0];
                        floorIndicators.RemoveAt(0);
                    }
                    else
                    {
                        line += longestIndicatorAsWhiteSpace;
                    }

                    var room = rooms[x, y, z];

                    if (room != null)
                    {
                        if (room.HasBeenVisited || region.VisibleWithoutDiscovery)
                            line += region.CurrentRoom == room ? CurrentRoomString : EmptyRoomString;
                        else
                            line += blankRoomRow;
                    }
                    else
                    {
                        line += blankRoomRow;
                    }

                    line += longestIndicatorAsWhiteSpace;
                }

                map += lineStringBuilder.BuildWrappedPadded(line, availableColumns, true);
            }

            return map;
        }

        /// <summary>
        /// Build a detailed map of a region.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="roomColumns">The width of a room, in columns.</param>
        /// <param name="firstRoomX">The X location within the region, in rooms, to begin building at.</param>
        /// <param name="firstRoomY">The Y location within the region, in rooms, to begin building at.</param>
        /// <param name="roomsToBuildX">The number of rooms to build in the X direction.</param>
        /// <param name="roomsToBuildY">The number of rooms to build in the Y direction.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string BuildDetailedRegionMap(Region region, LineStringBuilder lineStringBuilder, int availableColumns, int roomColumns, int firstRoomX, int firstRoomY, int roomsToBuildX, int roomsToBuildY)
        {
            var map = string.Empty;
            var blankRoomRow = lineStringBuilder.BuildWhitespace(roomColumns);
            var rooms = region.ToMatrix();
            var currentRoomPosition = region.GetPositionOfRoom(region.CurrentRoom);
            var z = currentRoomPosition?.Z ?? 0;
            var floors = rooms.Depth;
            var floorIndicators = BuildFloorIndicators(region, floors, lineStringBuilder);
            var longestIndicatorAsWhitespace = lineStringBuilder.BuildWhitespace(floorIndicators.Max(x => x.Length));

            for (var y = roomsToBuildY - 1; y >= firstRoomY; y--)
            {
                for (var yPass = 0; yPass < 3; yPass++)
                {
                    var line = string.Empty;

                    if (floorIndicators.Any())
                    {
                        line += floorIndicators[0];
                        floorIndicators.RemoveAt(0);
                    }
                    else
                    {
                        line += longestIndicatorAsWhitespace;
                    }

                    for (var x = firstRoomX; x < roomsToBuildX; x++)
                    {
                        var room = rooms[x, y, z];

                        if (room != null)
                        {
                            if (room.HasBeenVisited || region.VisibleWithoutDiscovery)
                                line += BuildRoomRowString(room, region.CurrentRoom == room, yPass);
                            else
                                line += blankRoomRow;
                        }
                        else
                        {
                            line += blankRoomRow;
                        }
                    }

                    line += longestIndicatorAsWhitespace;

                    map += lineStringBuilder.BuildWrappedPadded(line, availableColumns, true);
                }
            }

            return map;
        }

        /// <summary>
        /// Build a string representing a row slice of a room.
        /// </summary>
        /// <param name="room">The room to get a slice of.</param>
        /// <param name="isPlayerInRoom">Specify if the player is in the room.</param>
        /// <param name="row">The slice of the row to get (0 = top, 1 = middle, 2 = bottom).</param>
        /// <returns>The room slice, as a string.</returns>
        private string BuildRoomRowString(Room room, bool isPlayerInRoom, int row)
        {
            switch (row)
            {
                case 0:
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(Direction.North) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.North) ? " " : HorizontalBoundaryString);
                case 1:
                    return $"{(room.HasLockedExitInDirection(Direction.West) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.West) ? " " : VerticalBoundaryString)}{(room.HasLockedExitInDirection(Direction.Up) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.Up) ? "^" : " ")}{(isPlayerInRoom ? CurrentRoomString : " ")}{(room.HasLockedExitInDirection(Direction.Down) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.Down) ? "v" : " ")}{(room.HasLockedExitInDirection(Direction.East) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.East) ? " " : VerticalBoundaryString)}";
                case 2:
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(Direction.South) ? LockedExitString : room.HasUnlockedExitInDirection(Direction.South) ? " " : HorizontalBoundaryString);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and get the bounds of a Region.
        /// </summary>
        /// <param name="region">The Region to check.</param>
        /// <param name="x">The x of the region.</param>
        /// <param name="y">The y of the region.</param>
        /// <param name="z">The z of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="depth">The depth of the region.</param>
        /// <returns>True if the bounds could be got, else false.</returns>
        private static bool TryGetRegionBounds(Region region, out int x, out int y, out int z, out int width, out int height, out int depth)
        {
            if (region != null)
            {
                var matrix = region.ToMatrix();

                x = 0;
                width = matrix.Width;
                y = 0;
                height = matrix.Height;
                z = 0;
                depth = matrix.Depth;
                return true;
            }

            x = 0;
            width = 0;
            y = 0;
            height = 0;
            z = 0;
            depth = 0;
            return false;
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="lineStringBuilder">The string builder to use.</param>
        /// <param name="region">The region.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="availableRows">The available vertical space, in rows, to build the map within.</param>
        /// <returns>A map of the region in a string.</returns>
        public string BuildRegionMap(LineStringBuilder lineStringBuilder, Region region, int availableColumns, int availableRows)
        {
            if (!TryGetRegionBounds(region, out var x, out var y, out _, out var width, out var height, out _))
                return string.Empty;

            var floors = region.ToMatrix().Depth - 1;
            var maxFloorIndicatorLength = floors > 0 ? BuildFloorIndicator(floors, true, lineStringBuilder).Length : 0;
            var roomWidthTotal = $"{VerticalBoundaryString}{HorizontalBoundaryString} {HorizontalBoundaryString}{VerticalBoundaryString}".Length;
            var roomHeightTotal = roomWidthTotal;
            var detailedWouldFit = (width - x) * roomWidthTotal + (maxFloorIndicatorLength * 2) < availableColumns - 2 && (height - y) * roomHeightTotal < availableRows;
            var undetailedWouldFit = width - x + (maxFloorIndicatorLength * 2) < availableColumns - 2 && height - y < availableRows;

            switch (RegionMapDetail)
            {
                case RegionMapMode.Detailed:

                    if (detailedWouldFit)
                        return BuildDetailedRegionMap(region, lineStringBuilder, availableColumns, roomWidthTotal, x, y, width, height);

                    return lineStringBuilder.BuildWrappedPadded("Region map cannot be displayed as it exceeds the viewable area.", availableColumns, false);

                case RegionMapMode.Dynamic:

                    if (detailedWouldFit)
                        return BuildDetailedRegionMap(region, lineStringBuilder, availableColumns, roomWidthTotal, x, y, width, height);

                    if (undetailedWouldFit)
                        return BuildUndetailedRegionMap(region, lineStringBuilder, availableColumns, x, y, width, height);

                    return lineStringBuilder.BuildWrappedPadded("Region map cannot be displayed as it exceeds the viewable area.", availableColumns, false);

                case RegionMapMode.Undetailed:

                    if (undetailedWouldFit)
                        return BuildUndetailedRegionMap(region, lineStringBuilder, availableColumns, x, y, width, height);

                    return lineStringBuilder.BuildWrappedPadded("Region map cannot be displayed as it exceeds the viewable area.", availableColumns, false);

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}
