using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Parsing.Commands
{
    /// <summary>
    /// Represents the Drop command.
    /// </summary>
    public class Drop : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the character who has the item.
        /// </summary>
        public PlayableCharacter Character { get; }

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Get the room to drop the item in.
        /// </summary>
        public Room Room { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Drop command.
        /// </summary>
        /// <param name="character">The character who has the item.</param>
        /// <param name="item">The item to take.</param>
        /// <param name="room">The room to drop the item in.</param>
        public Drop(PlayableCharacter character, Item item, Room room)
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
                return new Reaction(ReactionResult.NoReaction, "You must specify what to drop.");

            if (!Character.HasItem(Item))
                return new Reaction(ReactionResult.NoReaction, "You don't have that item.");

            Room.AddItem(Item);
            Character.DequireItem(Item);
            return new Reaction(ReactionResult.Reacted, $"Dropped {Item.Identifier.Name}.");
        }

        #endregion
    }
}
