namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Enumeration of reactions to input.
    /// </summary>
    public enum ReactionToInput
    {
        /// <summary>
        /// Could react to input.
        /// </summary>
        CouldReact = 0,
        /// <summary>
        /// Couldn't react to input.
        /// </summary>
        CouldntReact,
        /// <summary>
        /// A self contained reaction to an input.
        /// </summary>
        SelfContainedReaction
    }
}
