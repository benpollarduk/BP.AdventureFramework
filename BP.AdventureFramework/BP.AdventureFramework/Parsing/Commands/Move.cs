using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Parsing.Commands
{
    /// <summary>
    /// Represents the Move command.
    /// </summary>
    public class Move : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the region.
        /// </summary>
        public Region Region { get; }

        /// <summary>
        /// Get the direction.
        /// </summary>
        public CardinalDirection Direction { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Move command.
        /// </summary>
        /// <param name="region">The region to move within.</param>
        /// <param name="direction">The direction to move.</param>
        public Move(Region region, CardinalDirection direction)
        {
            Region = region;
            Direction = direction;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Region.Move(Direction))
                return new Reaction(ReactionResult.Reacted, $"Moved {Direction}.");

            return new Reaction(ReactionResult.NoReaction, $"Could not move {Direction}.");
        }

        #endregion
    }
}
