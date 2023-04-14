using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Parsing.Commands;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Take command.
    /// </summary>
    public class Take : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the character will acquire the item.
        /// </summary>
        public PlayableCharacter Character { get; }

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Get the room to take the item from.
        /// </summary>
        public Room Room { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Take command.
        /// </summary>
        /// <param name="character">The character who will acquire the item.</param>
        /// <param name="item">The item to take.</param>
        /// <param name="room">The room to take the item from.</param>
        public Take(PlayableCharacter character, Item item, Room room)
        {
            Character = character;
            Item = item;
            Room = room;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (Character == null)
                return new Reaction(ReactionResult.None, "You must specify a character.");

            if (Item == null)
                return new Reaction(ReactionResult.None, "You must specify what to take.");

            if (!Room.ContainsItem(Item))
                return new Reaction(ReactionResult.None, "The room does not contain that item.");

            if (!Item.IsTakeable)
                return new Reaction(ReactionResult.None, $"{Item.Identifier.Name} cannot be taken.");

            Room.RemoveItemFromRoom(Item);
            Character.AquireItem(Item);

            return new Reaction(ReactionResult.Reacted, $"Took {Item.Identifier.Name}");
        }

        #endregion
    }
}
