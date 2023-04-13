using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Parsing.Commands
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
                return new Reaction(ReactionResult.NoReaction, "You must specify a character.");

            if (Item == null)
                return new Reaction(ReactionResult.NoReaction, "You must specify what to take.");

            if (!Room.ContainsItem(Item))
                return new Reaction(ReactionResult.NoReaction, "The room does not contain that item.");

            if (!Item.IsTakeable)
                return new Reaction(ReactionResult.NoReaction, $"{Item.Identifier.Name} cannot be taken.");

            Room.RemoveItemFromRoom(Item);
            Character.AquireItem(Item);

            return new Reaction(ReactionResult.Reacted, $"Took {Item.Identifier.Name}");
        }

        #endregion
    }
}
