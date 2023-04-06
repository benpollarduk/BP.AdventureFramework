using System;

namespace BP.AdventureFramework.Structure
{
    /// <summary>
    /// Represents event arguments for GameIO events.
    /// </summary>
    public class GameIOEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the Game.
        /// </summary>
        public Game Game { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameIOEventArgs class.
        /// </summary>
        /// <param name="game">The Game.</param>
        public GameIOEventArgs(Game game)
        {
            Game = game;
        }

        #endregion
    }
}