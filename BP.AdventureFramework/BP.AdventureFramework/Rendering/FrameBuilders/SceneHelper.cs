using System;
using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides helper functionality for scenes.
    /// </summary>
    internal static class SceneHelper
    {
        /// <summary>
        /// Create a view point string.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The view point.</param>
        /// <returns>The view point, as a string.</returns>
        internal static string CreateViewpointAsString(Room room, ViewPoint viewPoint)
        {
            var view = string.Empty;

            foreach (var direction in new[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.Up, Direction.Down })
            {
                var roomInDirection = viewPoint[direction];

                if (roomInDirection == null)
                    continue;

                var roomDescription = room[direction].IsLocked ? "a locked exit" : $"the {roomInDirection.Identifier.Name}";

                if (string.IsNullOrEmpty(view))
                {
                    switch (direction)
                    {
                        case Direction.North:
                        case Direction.East:
                        case Direction.South:
                        case Direction.West:
                            view += $"To the {direction.ToString().ToLower()} is {roomDescription}, ";
                            break;
                        case Direction.Up:
                            view += $"Above is {roomDescription}, ";
                            break;
                        case Direction.Down:
                            view += $"Below is {roomDescription}, ";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    view += $"{direction.ToString().ToLower()} is {roomDescription}, ";
                }
            }

            return string.IsNullOrEmpty(view) ? string.Empty : view.Remove(view.Length - 2).EnsureFinishedSentence();
        }

        /// <summary>
        /// Create a description of the NPC's as a string.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The characters, as a string.</returns>
        internal static string CreateNPCString(Room room)
        {
            var characters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (!characters.Any())
                return string.Empty;

            if (characters.Length == 1)
                return characters[0].Identifier + " is in this area.";

            var charactersAsString = string.Empty;

            foreach (var character in characters)
                charactersAsString += character.Identifier + ", ";

            charactersAsString = charactersAsString.Remove(characters.Length - 2);
            return charactersAsString.Substring(0, charactersAsString.LastIndexOf(",", StringComparison.Ordinal)) + " and " + charactersAsString.Substring(charactersAsString.LastIndexOf(",", StringComparison.Ordinal) + 2) + " are in the " + room.Identifier + ".";
        }
    }
}
