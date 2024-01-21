using System;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents a playable character.
    /// </summary>
    public sealed class PlayableCharacter : Character
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(string identifier, string description, params Item[] items) : this(new Identifier(identifier), new Description(description), items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(Identifier identifier, Description description, params Item[] items)
        {
            Identifier = identifier;
            Description = description;
            Items = items ?? Array.Empty<Item>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="targetObject">A target object to use the item on.</param>
        /// <param name="item">The item to use.</param>
        /// <returns>The result of the items usage.</returns>
        public InteractionResult UseItem(IInteractWithItem targetObject, Item item)
        {
            var result = targetObject.Interact(item);

            if (result.Effect == InteractionEffect.FatalEffect)
                IsAlive = false;

            return result;
        }

        #endregion
    }
}