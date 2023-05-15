using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Take command.
    /// </summary>
    internal class Take : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Take command.
        /// </summary>
        /// <param name="item">The item to take.</param>
        public Take(Item item)
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
                return new Reaction(ReactionResult.Error, "You must specify what to take.");

            if (!game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(Item))
                return new Reaction(ReactionResult.Error, "The room does not contain that item.");

            if (!Item.IsTakeable)
                return new Reaction(ReactionResult.Error, $"{Item.Identifier.Name} cannot be taken.");

            game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(Item);
            game.Player.AquireItem(Item);

            return new Reaction(ReactionResult.OK, $"Took {Item.Identifier.Name}");
        }

        #endregion
    }
}
