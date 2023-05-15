using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOff command.
    /// </summary>
    internal class KeyOff : ICommand
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

            game.SceneMapKeyType = KeyType.None;
            return new Reaction(ReactionResult.OK, "Key has been turned off.");
        }

        #endregion
    }
}