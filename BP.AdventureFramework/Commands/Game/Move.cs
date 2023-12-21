using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Move command.
    /// </summary>
    internal class Move : ICommand
    {
        #region Constants

        /// <summary>
        /// Get the prefix for successful moves.
        /// </summary>
        public const string SuccessfulMovePrefix = "Moved";

        #endregion

        #region Properties

        /// <summary>
        /// Get the direction.
        /// </summary>
        public Direction Direction { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Move command.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        public Move(Direction direction)
        {
            Direction = direction;
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

            if (game.Overworld.CurrentRegion.Move(Direction))
                return new Reaction(ReactionResult.OK, $"{SuccessfulMovePrefix} {Direction}.");

            return new Reaction(ReactionResult.Error, $"Could not move {Direction}.");
        }

        #endregion
    }
}
