namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents anything that is examinable.
    /// </summary>
    public interface IExaminable
    {
        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        Identifier Identifier { get; }
        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        Description Description { get; set; }
        /// <summary>
        /// Get if this is visible to the player.
        /// </summary>
        bool IsPlayerVisible { get; set; }
        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        ExaminationResult Examime();
    }
}