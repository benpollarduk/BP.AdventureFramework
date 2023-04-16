using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOff command.
    /// </summary>
    internal class KeyOff : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Logic.Game Game { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the KeyOff class.
        /// </summary>
        /// <param name="game">The game.</param>
        public KeyOff(Logic.Game game)
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

            Game.MapKeyType = KeyType.None;
            return new Reaction(ReactionResult.Reacted, "Key has been turned off.");
        }

        #endregion
    }
}