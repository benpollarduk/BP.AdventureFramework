using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.Parsing.Commands;

namespace BP.AdventureFramework.Commands.Global
{
    /// <summary>
    /// Represents the New command.
    /// </summary>
    public class New : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public GameStructure.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the New class.
        /// </summary>
        /// <param name="game">The game.</param>
        public New(GameStructure.Game game)
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

            Game.Refresh(Game.TitleFrame);
            return new Reaction(ReactionResult.Reacted, "New game.");
        }

        #endregion
    }
}