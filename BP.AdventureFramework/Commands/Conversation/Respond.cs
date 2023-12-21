using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Conversations;

namespace BP.AdventureFramework.Commands.Conversation
{
    /// <summary>
    /// Represents the Respond command.
    /// </summary>
    internal class Respond : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the conversation response.
        /// </summary>
        public Response Response { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Respond command.
        /// </summary>
        /// <param name="response">The response.</param>
        public Respond(Response response)
        {
            Response = response;
        }

        #endregion

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

            if (Response == null)
                return new Reaction(ReactionResult.Error, "No response specified.");

            if (game.ActiveConverser?.Conversation == null)
                return new Reaction(ReactionResult.Error, "No active conversation.");

            return game.ActiveConverser.Conversation.Respond(Response, game);
        }

        #endregion
    }
}