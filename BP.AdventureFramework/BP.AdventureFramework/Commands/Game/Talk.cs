using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Talk command.
    /// </summary>
    internal class Talk : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the converser.
        /// </summary>
        public IConverser Converser { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Talk command.
        /// </summary>
        /// <param name="converser">The converser.</param>
        public Talk(IConverser converser)
        {
            Converser = converser;
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

            if (Converser == null)
                return new Reaction(ReactionResult.Error, "No-one is around to talk to.");

            if (Converser is Character character && !character.IsAlive)
                return new Reaction(ReactionResult.Error, $"{character.Identifier.Name} is dead.");

            game.StartConversation(Converser);
            return new Reaction(ReactionResult.Internal, "Engaged in conversation.");
        }

        #endregion
    }
}
