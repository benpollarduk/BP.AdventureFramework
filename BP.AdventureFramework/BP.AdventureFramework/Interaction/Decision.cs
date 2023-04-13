namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a boolean decision.
    /// </summary>
    public class Decision
    {
        #region Properties

        /// <summary>
        /// Get the result of the Decision.
        /// </summary>
        public ReactionResult Result { get; protected set; }

        /// <summary>
        /// Get a reason for this Decision.
        /// </summary>
        public string Reason { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Decision class.
        /// </summary>
        /// <param name="result">The result of the decision.</param>
        public Decision(ReactionResult result)
        {
            Result = result;
            Reason = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Decision class.
        /// </summary>
        /// <param name="result">The result of the decision.</param>
        /// <param name="reason">The reason for this decision.</param>
        public Decision(ReactionResult result, string reason)
        {
            Result = result;
            Reason = reason;
        }

        #endregion
    }
}