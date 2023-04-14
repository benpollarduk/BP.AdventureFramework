using System;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;

namespace BP.AdventureFramework.Parsing.Commands.Game
{
    /// <summary>
    /// Represents the UseOn command.
    /// </summary>
    public class UseOn : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Get the target.
        /// </summary>
        public IInteractWithItem Target { get; }

        /// <summary>
        /// Get the character using this item.
        /// </summary>
        public PlayableCharacter Character { get; }

        /// <summary>
        /// Get the room this item is being used within.
        /// </summary>
        public Room Room { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UseOn command.
        /// </summary>
        /// <param name="item">The item to use.</param>
        /// <param name="target">The target of the command.</param>
        /// <param name="character">The character using this item.</param>
        /// <param name="room">The room the item is being used within.</param>
        public UseOn(Item item, IInteractWithItem target, PlayableCharacter character, Room room)
        {
            Item = item;
            Target = target;
            Character = character;
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
            if (Item == null)
                return new Reaction(ReactionResult.NoReaction, "You must specify an item.");

            if (Target == null)
                return new Reaction(ReactionResult.NoReaction, "You must specify a target.");

            if (Character == null)
                return new Reaction(ReactionResult.NoReaction, "You must specify the character that is using this item.");

            var result = Target.Interact(Item);

            switch (result.Effect)
            {
                case InteractionEffect.FatalEffect:

                    Character.Kill(result.Desciption);
                    return new Reaction(ReactionResult.Reacted, result.Desciption);

                case InteractionEffect.ItemUsedUp:

                    if (Room.ContainsItem(Item))
                        Room.RemoveItemFromRoom(Item);
                    else if (Character.HasItem(Item))
                        Character.DequireItem(Item);

                    break;

                case InteractionEffect.TargetUsedUp:

                    var examinable = Target as IExaminable;

                    if (examinable != null)
                    {
                        if (Room.ContainsInteractionTarget(examinable.Identifier.Name))
                            Room.RemoveInteractionTargetFromRoom(Target);
                    }

                    break;

                case InteractionEffect.NoEffect:
                case InteractionEffect.ItemMorphed:
                case InteractionEffect.SelfContained:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new Reaction(ReactionResult.Reacted, result.Desciption);
        }

        #endregion
    }
}