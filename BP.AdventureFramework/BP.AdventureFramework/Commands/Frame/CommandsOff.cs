using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOff command.
    /// </summary>
    internal class CommandsOff : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new Reaction(ReactionResult.Error, "No game specified.");

            game.DisplayCommandListInSceneFrames = false;
            return new Reaction(ReactionResult.OK, "Commands have been turned off.");
        }

        #endregion
    }
}