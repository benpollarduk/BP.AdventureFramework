using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Drop command.
    /// </summary>
    internal class Drop : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Drop command.
        /// </summary>
        /// <param name="item">The item to take.</param>
        public Drop(Item item)
        {
            Item = item;
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

            if (game.Player == null)
                return new Reaction(ReactionResult.Error, "You must specify a character.");

            if (Item == null)
                return new Reaction(ReactionResult.Error, "You must specify what to drop.");

            if (!game.Player.HasItem(Item))
                return new Reaction(ReactionResult.Error, "You don't have that item.");

            game.Overworld.CurrentRegion.CurrentRoom.AddItem(Item);
            game.Player.DequireItem(Item);

            return new Reaction(ReactionResult.OK, $"Dropped {Item.Identifier.Name}.");
        }

        #endregion
    }
}
