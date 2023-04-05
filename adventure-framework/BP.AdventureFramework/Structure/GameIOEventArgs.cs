using System;

namespace AdventureFramework.Structure
{
    /// <summary>
    /// Represents event arguments for GameIO events
    /// </summary>
    public class GameIOEventArgs : EventArgs
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameIOEventArgs class
        /// </summary>
        /// <param name="game">The Game to pass in the args</param>
        public GameIOEventArgs(Game game)
        {
            // set game
            Game = game;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the Game passed by these args
        /// </summary>
        public Game Game
        {
            get { return game; }
            protected set { game = value; }
        }

        /// <summary>
        /// Get or set the Game passed by these args
        /// </summary>
        private Game game;

        #endregion
    }

    /// <summary>
    /// Event handler for Game I/O events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void GameIOEventHandler(object sender, GameIOEventArgs e);
}