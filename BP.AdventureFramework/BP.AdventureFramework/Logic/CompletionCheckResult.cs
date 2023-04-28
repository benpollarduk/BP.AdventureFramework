namespace BP.AdventureFramework.Logic
{
    /// <summary>
    /// Represents the result of a completion check.
    /// </summary>
    public class CompletionCheckResult
    {
        #region StaticProperties

        /// <summary>
        /// Get a default result for not complete.
        /// </summary>
        public static CompletionCheckResult NotComplete { get; } = new CompletionCheckResult(false, string.Empty, string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get if the game has been completed.
        /// </summary>
        public bool IsCompleted { get; }

        /// <summary>
        /// Get a title to describe the completion.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Get a description of the completion.
        /// </summary>
        public string Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CompletionCheckResult class.
        /// </summary>
        /// <param name="isCompleted">If the game has been completed.</param>
        /// <param name="title">A title to describe the completion.</param>
        /// <param name="description">A description of the completion.</param>
        public CompletionCheckResult(bool isCompleted, string title, string description)
        {
            IsCompleted = isCompleted;
            Title = title;
            Description = description;
        }

        #endregion
    }
}
