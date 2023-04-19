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
                    Desciption = "There was a fatal effect.";
                    break;
                case InteractionEffect.ItemMorphed:
                    Desciption = "The item morphed.";
                    break;
                case InteractionEffect.ItemUsedUp:
                    Desciption = "The item was used up.";
                    break;
                case InteractionEffect.NoEffect:
                    Desciption = "There was no effect.";
                    break;
                case InteractionEffect.SelfContained:
                    Desciption = "The effect was self contained.";
                    break;
                case InteractionEffect.TargetUsedUp:
                    Desciption = "The target was used up.";
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
            Desciption = descriptionOfEffect;
        }

        #endregion
    }
}