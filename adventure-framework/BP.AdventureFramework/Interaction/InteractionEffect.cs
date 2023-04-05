namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Enumeration of interaction effects.
    /// </summary>
    public enum InteractionEffect
    {
        /// <summary>
        /// No effect to the interaction on either the item or the target.
        /// </summary>
        NoEffect = 0,
        /// <summary>
        /// Item was used up.
        /// </summary>
        ItemUsedUp,
        /// <summary>
        /// Item morphed into another object.
        /// </summary>
        ItemMorphed,
        /// <summary>
        /// A fatal effect to the interaction.
        /// </summary>
        FatalEffect,
        /// <summary>
        /// The target was used up.
        /// </summary>
        TargetUsedUp,
        /// <summary>
        /// Any other self contained effect.
        /// </summary>
        SelfContained
    }
}