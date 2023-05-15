using System;

namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Represents a result of an interaction.
    /// </summary>
    public sealed class InteractionResult : Result
    {
        #region Properties

        /// <summary>
        /// Get the effect.
        /// </summary>
        public InteractionEffect Effect { get; }

        /// <summary>
        /// Get the item used in the interaction.
        /// </summary>
        public Item Item { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InteractionResult class.
        /// </summary>
        /// <param name="effect">The effect of this interaction.</param>
        /// <param name="item">The item used in this interaction.</param>
        public InteractionResult(InteractionEffect effect, Item item)
        {
            Effect = effect;
            Item = item;

            switch (effect)
            {
                case InteractionEffect.FatalEffect:
                    Description = "There was a fatal effect.";
                    break;
                case InteractionEffect.ItemMorphed:
                    Description = "The item morphed.";
                    break;
                case InteractionEffect.ItemUsedUp:
                    Description = "The item was used up.";
                    break;
                case InteractionEffect.NoEffect:
                    Description = "There was no effect.";
                    break;
                case InteractionEffect.SelfContained:
                    Description = "The effect was self contained.";
                    break;
                case InteractionEffect.TargetUsedUp:
                    Description = "The target was used up.";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the InteractionResult class.
        /// </summary>
        /// <param name="effect">The effect of this interaction.</param>
        /// <param name="item">The item used in this interaction.</param>
        /// <param name="descriptionOfEffect">A description of the effect.</param>
        public InteractionResult(InteractionEffect effect, Item item, string descriptionOfEffect)
        {
            Effect = effect;
            Item = item;
            Description = descriptionOfEffect;
        }

        #endregion
    }
}