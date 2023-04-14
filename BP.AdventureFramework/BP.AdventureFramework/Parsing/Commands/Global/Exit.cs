using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Parsing.Commands.Global
{
    /// <summary>
    /// Represents the Exit command.
    /// </summary>
    public class Exit : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public GameStructure.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Exit class.
        /// </summary>
        /// <param name="game">The game.</param>
        public Exit(GameStructure.Game game)
        {
            Game = game;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Game == null)
                return new Reaction(ReactionResult.NoReaction, "No game specified.");

            Game.End();
            return new Reaction(ReactionResult.Reacted, "Exiting...");
        }

        #endregion
    }
}