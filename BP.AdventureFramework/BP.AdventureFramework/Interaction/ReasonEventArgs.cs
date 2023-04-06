using System;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Event arguments for reason events.
    /// </summary>
    public class ReasonEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the reason.
        /// </summary>
        public string Reason { get; protected set; } = string.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ReasonEventArgs class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public ReasonEventArgs(string reason)
        {
            Reason = reason;
        }

        #endregion
    }
}