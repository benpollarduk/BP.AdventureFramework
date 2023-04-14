using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Parsing.Commands.Global
{
    /// <summary>
    /// Represents the Help command.
    /// </summary>
    public class Help : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public GameStructure.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Help class.
        /// </summary>
        /// <param name="game">The game.</param>
        public Help(GameStructure.Game game)
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
                return new Reaction(ReactionResult.None, "No game specified.");

            Game.Refresh(Game.HelpFrame);
            return new Reaction(ReactionResult.SelfContained, string.Empty);
        }

        #endregion
    }
}