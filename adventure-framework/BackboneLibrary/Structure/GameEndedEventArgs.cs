using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Event arguments for end of game events
    /// </summary>
    public class GameEndedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the exit mode
        /// </summary>
        public EExitMode ExitMode
        {
            get { return this.exitMode; }
            protected set { this.exitMode = value; }
        }

        /// <summary>
        /// Get or set the exit mode
        /// </summary>
        private EExitMode exitMode = EExitMode.ExitApplication;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameEndedEventArgs class
        /// </summary>
        /// <param name="mode">The type of exit to use</param>
        public GameEndedEventArgs(EExitMode mode)
        {
            // set the exit mode
            this.ExitMode = mode;
        }

        #endregion
    }

    /// <summary>
    /// Event handler for game ended events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public  delegate void GameEndedEventHandler(object sender, GameEndedEventArgs e);

    /// <summary>
    /// Enumeration of exit modes
    /// </summary>
    public enum EExitMode
    {
        /// <summary>
        /// Exit the application
        /// </summary>
        ExitApplication = 0,
        /// <summary>
        /// Return to the title screen
        /// </summary>
        ReturnToTitleScreen
    }
}
