using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOn command.
    /// </summary>
    internal class CommandsOn : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Logic.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandsOn class.
        /// </summary>
        /// <param name="game">The game.</param>
        public CommandsOn(Logic.Game game)
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

            Game.DisplayCommandListInSceneFrames = true;
            return new Reaction(ReactionResult.Reacted, "Commands have been turned on.");
        }

        #endregion
    }
}