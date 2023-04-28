using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOn command.
    /// </summary>
    internal class KeyOn : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Logic.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyOn class.
        /// </summary>
        /// <param name="game">The game.</param>
        public KeyOn(Logic.Game game)
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

            Game.SceneMapKeyType = KeyType.Dynamic;
            return new Reaction(ReactionResult.Reacted, "Key has been turned on.");
        }

        #endregion
    }
}