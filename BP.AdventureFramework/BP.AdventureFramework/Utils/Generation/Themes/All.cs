using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Utils.Generation.Simple;

namespace BP.AdventureFramework.Utils.Generation.Themes
{
    /// <summary>
    /// Provides the all theme.
    /// </summary>
    public sealed class All : ITheme
    {
        #region StaticProperties

        /// <summary>
        /// Get the dungeon theme.
        /// </summary>
        private static ITheme Dungeon { get; } = new Dungeon();

        /// <summary>
        /// Get the forest theme.
        /// </summary>
        private static ITheme Forest { get; } = new Forest();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Combine multiple string arrays.
        /// </summary>
        /// <param name="values">The string arrays to combine.</param>
        /// <returns>The combined array.</returns>
        private static string[] Combine(params string[][] values)
        {
            var list = new List<string>();

            foreach (var value in values)
                list.AddRange(value);

            return list.Distinct().ToArray();
        }

        #endregion

        #region Implementation of ITheme

        /// <summary>
        /// Get the room nouns.
        /// </summary>
        public string[] RoomNouns => Combine(Dungeon.RoomNouns, Forest.RoomNouns);

        /// <summary>
        /// Get the room adjectives.
        /// </summary>
        public string[] RoomAdjectives => Combine(Dungeon.RoomAdjectives, Forest.RoomAdjectives);

        /// <summary>
        /// Get the takeable item nouns.
        /// </summary>
        public string[] TakeableItemNouns => Combine(Dungeon.TakeableItemNouns, Forest.TakeableItemNouns);

        /// <summary>
        /// Get the takeable item adjectives.
        /// </summary>
        public string[] TakeableItemAdjectives => Combine(Dungeon.TakeableItemAdjectives, Forest.TakeableItemAdjectives);

        /// <summary>
        /// Get the non-takeable item nouns.
        /// </summary>
        public string[] NonTakeableItemNouns => Combine(Dungeon.NonTakeableItemNouns, Forest.NonTakeableItemNouns);

        /// <summary>
        /// Get the non-takeable item adjectives.
        /// </summary>
        public string[] NonTakeableItemAdjectives => Combine(Dungeon.NonTakeableItemAdjectives, Forest.NonTakeableItemAdjectives);

        #endregion
    }
}
