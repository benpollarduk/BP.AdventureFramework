namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents the result of an examination.
    /// </summary>
    public class ExaminationResult : Result
    {
        #region Properties

        /// <summary>
        /// Get the type of result.
        /// </summary>
        public ExaminationResults Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class.
        /// </summary>
        /// <param name="description">A description of the result.</param>
        public ExaminationResult(string description)
        {
            Desciption = description;
            Type = ExaminationResults.DescriptionReturned;
        }

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class.
        /// </summary>
        /// <param name="description">A description of the result.</param>
        /// <param name="type">The type of this result.</param>
        public ExaminationResult(string description, ExaminationResults type) : this(description)
        {
            Type = type;
        }

        #endregion
    }
}