using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOff command.
    /// </summary>
    internal class CommandsOff : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Logic.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandsOff class.
        /// </summary>
        /// <param name="game">The game.</param>
        public CommandsOff(Logic.Game game)
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
            return new Reaction(ReactionResult.Reacted, "Commands have been turned off.");
        }

        #endregion
    }
}