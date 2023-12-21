using System.Linq;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Take all command.
    /// </summary>
    internal class TakeAll : ICommand
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

            if (game.Player == null)
                return new Reaction(ReactionResult.Error, "You must specify a character.");

            if (game.Overworld.CurrentRegion.CurrentRoom == null)
                return new Reaction(ReactionResult.Error, "Not in a room.");

            var itemsAsString = string.Empty;

            foreach (var item in game.Overworld.CurrentRegion.CurrentRoom.Items.Where(x => x.IsTakeable && x.IsPlayerVisible))
            {
                game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                game.Player.AcquireItem(item);

                itemsAsString += $"{item.Identifier.Name}, ";
            }

            if (!string.IsNullOrEmpty(itemsAsString))
            {
                itemsAsString = itemsAsString.Remove(itemsAsString.Length - 2);
                itemsAsString = $"Took {itemsAsString}.";
                return new Reaction(ReactionResult.OK, itemsAsString);
            }
            else
            {
                return new Reaction(ReactionResult.Error, "Nothing to take.");
            }
        }

        #endregion
    }
}
