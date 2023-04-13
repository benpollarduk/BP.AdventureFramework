using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    public class ConditionalDescription : Description
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
    }
}