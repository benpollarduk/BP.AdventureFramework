using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a boolean decision
    /// </summary>
    public class Decision
    {
        #region Properties

        /// <summary>
        /// Get the result of the Decision
        /// </summary>
        public EReactionToInput Result
        {
            get { return this.result; }
            protected set { this.result = value; }
        }

        /// <summary>
        /// Get or set the result of the Decision
        /// </summary>
        private EReactionToInput result;

        /// <summary>
        /// Get a reason for this Decision
        /// </summary>
        public String Reason
        {
            get { return this.reason; }
            protected set { this.reason = value; }
        }

        /// <summary>
        /// Get or set a reason for this Decision
        /// </summary>
        private String reason;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Decision class
        /// </summary>
        /// <param name="result">The result of the decision</param>
        public Decision(EReactionToInput result)
        {
            // set decision
            this.Result = result;

            // set reason
            this.Reason = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Decision class
        /// </summary>
        /// <param name="result">The result of the decision</param>
        /// <param name="reason">The reason for this decision</param>
        public Decision(EReactionToInput result, String reason)
        {
            // set decision
            this.Result = result;

            // set reason
            this.Reason = reason;
        }

        #endregion
    }

    /// <summary>
    /// Enuemration of reactions to input
    /// </summary>
    public enum EReactionToInput
    {
        /// <summary>
        /// Could react to input
        /// </summary>
        CouldReact = 0,
        /// <summary>
        /// Couldn't react to input
        /// </summary>
        CouldntReact,
        /// <summary>
        /// A self contained reaction to an input
        /// </summary>
        SelfContainedReaction
    }
}
