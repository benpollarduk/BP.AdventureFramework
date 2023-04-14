using BP.AdventureFramework.GameAssets.Interaction;

namespace BP.AdventureFramework.GameAssets
{
    /// <summary>
    /// Represents an object that can be examined.
    /// </summary>
    public class ExaminableObject : IExaminable
    {
        #region Properties

        /// <summary>
        /// Get or set the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; set; } = obj => new ExaminationResult(obj.Description != null ? obj.Description.GetDescription() : obj.GetType().Name);

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get this objects identifier.
        /// </summary>
        public Identifier Identifier { get; protected set; }

        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get or set if this is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public virtual ExaminationResult Examime()
        {
            return Examination(this);
        }

        #endregion
    }
}