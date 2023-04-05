using System;

namespace BP.AdventureFramework.Interaction
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
        public string Reason
        {
            get { return reason; }
            protected set { reason = value; }
        }

        /// <summary>
        /// Get or set the reason
        /// </summary>
        private string reason = string.Empty;

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
        public ReasonEventArgs(string reason)
        {
            // set reason
            Reason = reason;
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