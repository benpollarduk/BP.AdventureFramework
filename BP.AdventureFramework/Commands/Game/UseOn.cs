using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the UseOn command.
    /// </summary>
    internal class UseOn : ICommand
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UseOn command.
        /// </summary>
        /// <param name="item">The item to use.</param>
        /// <param name="target">The target of the command.</param>
        public UseOn(Item item, IInteractWithItem target)
        {
            Item = item;
            Target = target;
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

            if (Item == null)
                return new Reaction(ReactionResult.Error, "You must specify an item.");

            if (Target == null)
                return new Reaction(ReactionResult.Error, "You must specify a target.");

            if (game.Player == null)
                return new Reaction(ReactionResult.Error, "You must specify the character that is using this item.");

            var result = Target.Interact(Item);

            switch (result.Effect)
            {
                case InteractionEffect.FatalEffect:

                    game.Player.Kill();
                    return new Reaction(ReactionResult.Fatal, result.Description);

                case InteractionEffect.ItemUsedUp:

                    if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(Item))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(Item);
                    else if (game.Player.HasItem(Item))
                        game.Player.DequireItem(Item);

                    break;

                case InteractionEffect.TargetUsedUp:

                    var examinable = Target as IExaminable;

                    if (examinable != null && game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(examinable.Identifier.Name))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTarget(Target);

                    break;

                case InteractionEffect.NoEffect:
                case InteractionEffect.ItemMorphed:
                case InteractionEffect.SelfContained:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new Reaction(ReactionResult.OK, result.Description);
        }

        #endregion
    }
}