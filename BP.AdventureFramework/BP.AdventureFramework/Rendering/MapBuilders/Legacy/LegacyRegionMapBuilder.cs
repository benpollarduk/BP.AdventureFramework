using System;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.MapBuilders
{
    /// <summary>
    /// Provides a legacy builder for region maps.
    /// </summary>
    public class LegacyRegionMapBuilder : IRegionMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get the drawer.
        /// </summary>
        protected Drawer Drawer { get; }

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
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string CurrentRoomString { get; set; } = "O";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string EmptyRoomString { get; set; } = "+";

        /// <summary>
        /// Get or set the visibility mode to use for Rooms.
        /// </summary>
        public RegionDisplayMode RoomVisibilityMode { get; set; } = RegionDisplayMode.AllRegion;

        /// <summary>
        /// Get or set the detail to use for a Region map.
        /// </summary>
        public RegionMapMode RegionMapDetail { get; } = RegionMapMode.Dynamic;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyRegionMapBuilder class.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        public LegacyRegionMapBuilder(Drawer drawer)
        {
            Drawer = drawer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build an undetailed Region map.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="firstRoomX">The X location within the region, in rooms, to begin building at.</param>
        /// <param name="firstRoomY">The Y location within the region, in rooms, to begin building at.</param>
        /// <param name="roomsToBuildX">The number of rooms to build in the X direction.</param>
        /// <param name="roomsToBuildY">The number of rooms to build in the Y direction.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string BuildUndetailedRegionMap(Region region, int availableColumns, int firstRoomX, int firstRoomY, int roomsToBuildX, int roomsToBuildY)
        {
            // small scale map, just dots...

            var map = string.Empty;
            const string blankRoomRow = " ";
            var rooms = region.ToMatrix();

            for (var row = roomsToBuildY - 1; row >= firstRoomY; row--)
            {
                var line = string.Empty;

                for (var column = firstRoomX; column < roomsToBuildX; column++)
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

                map += Drawer.ConstructWrappedPaddedString(line, availableColumns, true);
            }

            return map;
        }

        /// <summary>
        /// Build a detailed map of a region.
        /// </summary>
        /// <param name="region">The Region to construct the map for.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="roomColumns">The width of a room, in columns.</param>
        /// <param name="firstRoomX">The X location within the region, in rooms, to begin building at.</param>
        /// <param name="firstRoomY">The Y location within the region, in rooms, to begin building at.</param>
        /// <param name="roomsToBuildX">The number of rooms to build in the X direction.</param>
        /// <param name="roomsToBuildY">The number of rooms to build in the Y direction.</param>
        /// <returns>A representation of the Region as a string.</returns>
        private string BuildDetailedRegionMap(Region region, int availableColumns, int roomColumns, int firstRoomX, int firstRoomY, int roomsToBuildX, int roomsToBuildY)
        {
            var map = string.Empty;
            var blankRoomRow = Drawer.ConstructWhitespaceString(roomColumns);
            var rooms = region.ToMatrix();

            for (var row = roomsToBuildY - 1; row >= firstRoomY; row--)
            {
                for (var rowPass = 0; rowPass < 3; rowPass++)
                {
                    var line = string.Empty;

                    for (var column = firstRoomX; column < roomsToBuildX; column++)
                    {
                        var room = rooms[column, row];

                        if (room != null)
                        {
                            if (room.HasBeenVisited || RoomVisibilityMode == RegionDisplayMode.AllRegion)
                                line += BuildRoomRowString(room, region.CurrentRoom == room, rowPass);
                            else
                                line += blankRoomRow;
                        }
                        else
                        {
                            line += blankRoomRow;
                        }
                    }

                    map += Drawer.ConstructWrappedPaddedString(line, availableColumns, true);
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
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(CardinalDirection.North) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.North) ? " " : HorizontalBoundaryString);
                case 1:
                    return $"{(room.HasLockedExitInDirection(CardinalDirection.West) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.West) ? " " : VerticalBoundaryString)} {(isPlayerInRoom ? CurrentRoomString : " ")} {(room.HasLockedExitInDirection(CardinalDirection.East) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.East) ? " " : VerticalBoundaryString)}";
                case 2:
                    return string.Format("{0}{1}{1}{1}{0}", VerticalBoundaryString, room.HasLockedExitInDirection(CardinalDirection.South) ? LockedExitString : room.HasUnlockedExitInDirection(CardinalDirection.South) ? " " : HorizontalBoundaryString);
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
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <returns>True if the bounds could be got, else false.</returns>
        private static bool TryGetRegionBounds(Region region, out int x, out int y, out int width, out int height)
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

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <param name="availableRows">The available vertical space, in rows, to build the map within.</param>
        /// <returns>A map of the region in a string.</returns>
        public string BuildRegionMap(Region region, int availableColumns, int availableRows)
        {
            if (!TryGetRegionBounds(region, out var x, out var y, out var width, out var height))
                return string.Empty;

            var roomWidthTotal = $"{VerticalBoundaryString}{HorizontalBoundaryString} {HorizontalBoundaryString}{VerticalBoundaryString}".Length;
            var roomHeightTotal = roomWidthTotal;
            var detailedWouldFit = (width - x) * roomWidthTotal < availableColumns - 2 && (height - y) * roomHeightTotal < availableRows;
            var undetailedWouldFit = width - x < availableColumns - 2 && height - y < availableRows;

            switch (RegionMapDetail)
            {
                case RegionMapMode.Detailed:

                    if (detailedWouldFit)
                        return BuildDetailedRegionMap(region, availableColumns, roomWidthTotal, x, y, width, height);

                    return Drawer.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", availableColumns);

                case RegionMapMode.Dynamic:

                    if (detailedWouldFit)
                        return BuildDetailedRegionMap(region, availableColumns, roomWidthTotal, x, y, width, height);

                    if (undetailedWouldFit)
                        return BuildUndetailedRegionMap(region, availableColumns, x, y, width, height);

                    return Drawer.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", availableColumns);

                case RegionMapMode.Undetailed:

                    if (undetailedWouldFit)
                        return BuildUndetailedRegionMap(region, availableColumns, x, y, width, height);

                    return Drawer.ConstructWrappedPaddedString("Region map cannot be displayed as it exceeds the viewable area.", availableColumns);

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}
