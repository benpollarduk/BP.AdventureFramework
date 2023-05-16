using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a legacy builder for room maps.
    /// </summary>
    public class LegacyRoomMapBuilder : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the string used for representing a locked exit.
        /// </summary>
        public string LockedExitString { get; set; } = "x";

        /// <summary>
        /// Get or set the string used for representing there is an item or character in the room.
        /// </summary>
        public string ItemOrCharacterInRoomString { get; set; } = "?";

        /// <summary>
        /// Get or set the string to use for vertical boundaries.
        /// </summary>
        public string VerticalBoundaryString { get; set; } = "|";

        /// <summary>
        /// Get or set the string to use for horizontal boundaries.
        /// </summary>
        public string HorizontalBoundaryString { get; set; } = "-";

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="lineStringBuilder">The line string builder to use.</param>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="availableColumns">The available horizontal space, in columns, to build the map within.</param>
        /// <returns>A string representing a map for the room.</returns>
        public string BuildRoomMap(LineStringBuilder lineStringBuilder, Room room, ViewPoint viewPoint, KeyType key, int availableColumns)
        {
            /*
             * |-|N|-|
             * - U D -
             * W  ?  E
             * -     -
             * |-|S|-|
             */

            if (availableColumns <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var map = string.Empty;
            var keyLines = new Queue<string>();
            var lockedExitString = $"{LockedExitString} = Locked Exit";
            var unlockedExitString = "N/E/S/W = Unlocked Exit";
            var entranceString = "n/e/s/w = Entrance";
            var itemsString = $"{ItemOrCharacterInRoomString} = Item(s) or Character(s) in Room";

            switch (key)
            {
                case KeyType.Dynamic:

                    if (room.UnlockedExits.Count(x => x.IsPlayerVisible) != room.Exits.Count(x => x.IsPlayerVisible))
                        keyLines.Enqueue($"  {lockedExitString}");

                    if (room.UnlockedExits.Any(x => x.IsPlayerVisible))
                        keyLines.Enqueue($"  {unlockedExitString}");

                    if (room.EnteredFrom.HasValue)
                        keyLines.Enqueue($"  {room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)} = Entrance");

                    if (room.Items.Any(x => x.IsPlayerVisible) || room.Characters.Any(x => x.IsPlayerVisible))
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

            var exitRepresentations = new Dictionary<Direction, string>();

            foreach (var direction in new[] { Direction.East, Direction.North, Direction.South, Direction.West })
            {
                if (room.EnteredFrom == direction)
                {
                    exitRepresentations.Add(direction, direction.ToString().ToLower().Substring(0, 1));
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
                        case Direction.East:
                        case Direction.West:
                            exitRepresentations.Add(direction, VerticalBoundaryString);
                            break;
                        case Direction.North:
                        case Direction.South:
                            exitRepresentations.Add(direction, HorizontalBoundaryString);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            foreach (var direction in new[] { Direction.Up, Direction.Down })
            {
                if (room.EnteredFrom == direction)
                {
                    exitRepresentations.Add(direction, direction.ToString().ToLower().Substring(0, 1));
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
                    exitRepresentations.Add(direction, " ");
                }
            }

            map += lineStringBuilder.BuildWrappedPadded($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[Direction.North] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += lineStringBuilder.BuildWrappedPadded($"{VerticalBoundaryString}{exitRepresentations[Direction.Up]}   {exitRepresentations[Direction.Down]}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += lineStringBuilder.BuildWrappedPadded(exitRepresentations[Direction.West] + "  " + (room.Items.Any(x => x.IsPlayerVisible) || room.Characters.Any(x => x.IsPlayerVisible) ? ItemOrCharacterInRoomString : " ") + "  " + exitRepresentations[Direction.East] + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += lineStringBuilder.BuildWrappedPadded($"{VerticalBoundaryString}     {VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);
            map += lineStringBuilder.BuildWrappedPadded($"{VerticalBoundaryString}{HorizontalBoundaryString}{HorizontalBoundaryString}" + exitRepresentations[Direction.South] + $"{HorizontalBoundaryString}{HorizontalBoundaryString}{VerticalBoundaryString}" + (keyLines.Count > 0 ? keyLines.Dequeue() : ""), availableColumns, false);

            return map;
        }

        #endregion
    }
}
