namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the result of an end check.
    /// </summary>
    public class EndCheckResult
    {
        #region StaticProperties

        /// <summary>
        /// Get a default result for not ended.
        /// </summary>
        public static EndCheckResult NotEnded { get; } = new EndCheckResult(false, string.Empty, string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get if the game has come to an end.
        /// </summary>
        public bool HasEnded { get; }

        /// <summary>
        /// Get a title to describe the end.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Get a description of the end.
        /// </summary>
        public string Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EndCheckResult class.
        /// </summary>
        /// <param name="hasEnded">If the game has ended.</param>
        /// <param name="title">A title to describe the end.</param>
        /// <param name="description">A description of the end.</param>
        public EndCheckResult(bool hasEnded, string title, string description)
        {
            HasEnded = hasEnded;
            Title = title;
            Description = description;
        }

        #endregion
    }
}
