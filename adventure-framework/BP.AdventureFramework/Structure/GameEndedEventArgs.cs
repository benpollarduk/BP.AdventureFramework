using System;

namespace BP.AdventureFramework.Structure
{
    /// <summary>
    /// Event arguments for end of game events.
    /// </summary>
    public class GameEndedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the exit mode.
        /// </summary>
        public ExitMode ExitMode { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameEndedEventArgs class.
        /// </summary>
        /// <param name="mode">The type of exit to use.</param>
        public GameEndedEventArgs(ExitMode mode)
        {
            ExitMode = mode;
        }

        #endregion
    }
}