using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Conversation
{
    /// <summary>
    /// Represents the End command.
    /// </summary>
    internal class End : ICommand
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

            game.EndConversation();
            return new Reaction(ReactionResult.OK, "Ended the conversation.");
        }

        #endregion
    }
}