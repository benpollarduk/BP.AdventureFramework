using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents the result of an examination.
    /// </summary>
    public class ExaminationResult : Result
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class.
        /// </summary>
        /// <param name="description">A description of the result.</param>
        public ExaminationResult(string description)
        {
            Description = description;
        }

        #endregion
    }
}