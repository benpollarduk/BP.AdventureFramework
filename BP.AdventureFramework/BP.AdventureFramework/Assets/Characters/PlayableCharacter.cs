using System;
using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents a playable character.
    /// </summary>
    public sealed class PlayableCharacter : Character
    {
        #region Properties

        /// <summary>
        /// Occurs if this player dies.
        /// </summary>
        public event EventHandler<string> Died;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        public PlayableCharacter(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="identifier">This PlayableCharacter's identifier.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(Identifier identifier, Description description, params Item[] items) : this(identifier, description)
        {
            Items.AddRange(items);
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

        /// <summary>
        /// Kill the character.
        /// </summary>
        /// <param name="reason">A reason for the death.</param>
        public override void Kill(string reason)
        {
            base.Kill(reason);
            Died?.Invoke(this, reason);
        }

        #endregion
    }
}