using BP.AdventureFramework.Assets.Interaction;

namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents an item that can be used within the game.
    /// </summary>
    public sealed class Item : ExaminableObject, IInteractWithItem
    {
        #region Properties

        /// <summary>
        /// Get or set if this is takeable.
        /// </summary>
        public bool IsTakeable { get; private set; }

        /// <summary>
        /// Get or set the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; set; } = (i, target) => new InteractionResult(InteractionEffect.NoEffect, i);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="identifier">This Items identifier.</param>
        /// <param name="description">A description of this Item.</param>
        /// <param name="isTakeable">Specify if this item is takeable.</param>
        public Item(Identifier identifier, Description description, bool isTakeable)
        {
            Identifier = identifier;
            Description = description;
            IsTakeable = isTakeable;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle item morphing.
        /// </summary>
        /// <param name="item">The item to morph into.</param>
        public void Morph(Item item)
        {
            Identifier = item.Identifier;
            Description = item.Description;
            IsPlayerVisible = item.IsPlayerVisible;
            IsTakeable = item.IsTakeable;
        }

        /// <summary>
        /// Use this item on a target.
        /// </summary>
        /// <param name="target">The target to use the item on.</param>
        /// <retunrs>The result of the interaction.</retunrs>
        public InteractionResult Use(IInteractWithItem target)
        {
            return target.Interact(this);
        }

        #endregion

        #region IInteractWithItem Members

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        public InteractionResult Interact(Item item)
        {
            return Interaction.Invoke(item, this);
        }

        #endregion
    }
}