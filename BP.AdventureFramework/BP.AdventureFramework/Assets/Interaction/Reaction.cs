namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Represents a reaction.
    /// </summary>
    public class Reaction
    {
        #region Properties

        /// <summary>
        /// Get the result.
        /// </summary>
        public ReactionResult Result { get; }

        /// <summary>
        /// Get a description of the result.
        /// </summary>
        public string Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Reaction class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="description">A description of the result.</param>
        public Reaction(ReactionResult result, string description)
        {
            Result = result;
            Description = description;
        }

        #endregion
    }
}
