namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Represents a result.
    /// </summary>
    public abstract class Result
    {
        #region Properties

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Result class.
        /// </summary>
        protected Result()
        {
            Description = "There was no effect.";
        }

        #endregion
    }
}