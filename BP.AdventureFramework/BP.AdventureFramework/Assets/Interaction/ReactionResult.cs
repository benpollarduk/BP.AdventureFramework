namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Enumeration of reaction results.
    /// </summary>
    public enum ReactionResult
    {
        /// <summary>
        /// Error.
        /// </summary>
        Error = 0,
        /// <summary>
        /// OK.
        /// </summary>
        OK,
        /// <summary>
        /// An internal reaction.
        /// </summary>
        Internal,
        /// <summary>
        /// A reaction that has a fatal effect on the player.
        /// </summary>
        Fatal
    }
}
