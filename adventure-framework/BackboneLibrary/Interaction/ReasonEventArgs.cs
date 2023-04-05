using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Event arguments for reason events
    /// </summary>
    public class ReasonEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the reason
        /// </summary>
        public String Reason
        {
            get { return this.reason; }
            protected set { this.reason = value; }
        }

        /// <summary>
        /// Get or set the reason
        /// </summary>
        private String reason = String.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ReasonEventArgs class
        /// </summary>
        protected ReasonEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReasonEventArgs class
        /// </summary>
        /// <param name="reason">The reason for the death</param>
        public ReasonEventArgs(String reason)
        {
            // set reason
            this.Reason = reason;
        }

        #endregion
    }

    /// <summary>
    /// Event handler for reason events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ReasonEventHandler(object sender, ReasonEventArgs e);
}
