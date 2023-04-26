namespace BP.AdventureFramework.Utils.Generation.Simple
{
    /// <summary>
    /// Represents a theme that can be used for simple generation.
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Get the room nouns.
        /// </summary>
        string[] RoomNouns { get; }
        /// <summary>
        /// Get the room adjectives.
        /// </summary>
        string[] RoomAdjectives { get; }
        /// <summary>
        /// Get the takeable item nouns.
        /// </summary>
        string[] TakeableItemNouns { get; }
        /// <summary>
        /// Get the takeable item adjectives.
        /// </summary>
        string[] TakeableItemAdjectives { get; }
        /// <summary>
        /// Get the non-takeable item nouns.
        /// </summary>
        string[] NonTakeableItemNouns { get; }
        /// <summary>
        /// Get the non-takeable item adjectives.
        /// </summary>
        string[] NonTakeableItemAdjectives { get; }
    }
}
