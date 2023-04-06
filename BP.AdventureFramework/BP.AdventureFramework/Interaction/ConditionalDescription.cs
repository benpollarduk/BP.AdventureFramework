using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    public class ConditionalDescription : Description, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the description for when this condition is false
        /// </summary>
        private string falseDescription;

        /// <summary>
        /// Get or set the condition
        /// </summary>
        public Condition Condition { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes anew instance of the ConditionalDescription class.
        /// </summary>
        /// <param name="trueDescription">The true description.</param>
        /// <param name="falseDescription">The false description.</param>
        /// <param name="condition">The condition.</param>
        public ConditionalDescription(string trueDescription, string falseDescription, Condition condition) : base(trueDescription)
        {
            this.falseDescription = falseDescription;
            Condition = condition;
        }

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description as a string.</returns>
        public override string GetDescription()
        {
            if (Condition != null)
                return Condition.Invoke() ? DefaultDescription : falseDescription;

            return DefaultDescription;
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ConditionalDescription.
        /// </summary>
        /// <returns>The ID as a string.</returns>
        public virtual string GenerateTransferalID()
        {
            return DefaultDescription + falseDescription;
        }

        /// <summary>
        /// Transfer delegation to this ConditionalDescription from a source ITransferableDelegation object.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public virtual void TransferFrom(ITransferableDelegation source)
        {
            Condition = ((ConditionalDescription)source).Condition;
        }

        /// <summary>
        /// Register all child properties of this ConditionalDescription that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ConditionalDescription.</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #endregion
    }
}