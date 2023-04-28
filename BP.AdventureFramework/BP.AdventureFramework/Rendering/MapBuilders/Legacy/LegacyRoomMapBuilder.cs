using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.LayoutBuilders;

namespace BP.AdventureFramework.Rendering.MapBuilders.Legacy
{
    /// <summary>
    /// Provides a legacy builder for room maps.
    /// </summary>
    public class LegacyRoomMapBuilder : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        protected IStringLayoutBuilder StringLayoutBuilder { get; }

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
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyRoomMapBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">The string layout builder.</param>
        public LegacyRoomMapBuilder(IStringLayoutBuilder stringLayoutBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <returns>A string representing a map for the room.</returns>
        public string BuildRoomMap(Room room, KeyType key, int availableColumns)
        {
            if (availableColumns <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var map = string.Empty;
            var keyLines = new Queue<string>();
            var lockedExitString = $"{LockedExitString}=Locked Exit";
            var unlockedExitString = "N/E/S/W = Unlocked Exit";
            var entranceString = "n/e/s/w = Entrance";
            var itemsString = $"{ItemInRoomString}=Item(s) In Room";

            switch (key)
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

            map += StringLayoutBuilder.BuildWrappedPadded($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[CardinalDirection.North] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += StringLayoutBuilder.BuildWrappedPadded($"{VerticalBoundaryString}     {VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += StringLayoutBuilder.BuildWrappedPadded(exitRepresentations[CardinalDirection.West] + "  " + (room.Items.Any() ? ItemInRoomString : " ") + "  " + exitRepresentations[CardinalDirection.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += StringLayoutBuilder.BuildWrappedPadded($"{VerticalBoundaryString}     {VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += StringLayoutBuilder.BuildWrappedPadded($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[CardinalDirection.South] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);

            return map;
        }

        #endregion
    }
}
