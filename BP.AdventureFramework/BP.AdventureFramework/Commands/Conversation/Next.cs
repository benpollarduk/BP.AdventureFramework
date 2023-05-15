using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Conversation
{
    /// <summary>
    /// Represents the Next command.
    /// </summary>
    internal class Next : ICommand
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

            if (game.ActiveConverser == null)
                return new Reaction(ReactionResult.Error, "No converser.");

            if (game.ActiveConverser.Conversation == null)
                return new Reaction(ReactionResult.Error, "No conversation.");

            return game.ActiveConverser.Conversation.Next(game);
        }

        #endregion
    }
}