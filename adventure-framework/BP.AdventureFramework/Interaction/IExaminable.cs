namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents anything that is examinable
    /// </summary>
    public interface IExaminable
    {
        #region Methods

        /// <summary>
        /// Examine this object
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object</returns>
        ExaminationResult Examime();

        #endregion

        #region Properties

        /// <summary>
        /// Get the name of this object
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get or set a description of this object
        /// </summary>
        Description Description { get; set; }

        /// <summary>
        /// Get if this is player visible
        /// </summary>
        bool IsPlayerVisible { get; set; }

        #endregion
    }
}