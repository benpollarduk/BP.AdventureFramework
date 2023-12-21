using System.Linq;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents a generic in game character.
    /// </summary>
    public abstract class Character : ExaminableObject, IInteractWithItem
    {
        #region Properties

        /// <summary>
        /// Get if this character is alive.
        /// </summary>
        public bool IsAlive { get; protected set; } = true;

        /// <summary>
        /// Get or set the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; set; } = (i, target) => new InteractionResult(InteractionEffect.NoEffect, i);

        /// <summary>
        /// Get the items this Character holds.
        /// </summary>
        public Item[] Items { get; protected set; } = new Item[0];

        #endregion

        #region Methods

        /// <summary>
        /// Interact with a specified item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        protected virtual InteractionResult InteractWithItem(Item item)
        {
            return Interaction.Invoke(item, this);
        }

        /// <summary>
        /// Kill the character.
        /// </summary>
        public virtual void Kill()
        {
            IsAlive = false;
        }

        /// <summary>
        /// Acquire an item.
        /// </summary>
        /// <param name="item">The item to acquire.</param>
        public virtual void AcquireItem(Item item)
        {
            Items = Items.Add(item);
        }

        /// <summary>
        /// De-acquire an item.
        /// </summary>
        /// <param name="item">The item to de-acquire.</param>
        public virtual void DequireItem(Item item)
        {
            Items = Items.Remove(item);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(Item item, bool includeInvisibleItems = false)
        {
            return Items.Contains(item) && (includeInvisibleItems || item.IsPlayerVisible);
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemName">The items name.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        public virtual bool FindItem(string itemName, out Item item, bool includeInvisibleItems = false)
        {
            var items = Items.Where(x => x.Identifier.Equals(itemName) && (includeInvisibleItems || x.IsPlayerVisible)).ToArray();

            if (items.Length > 0)
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        /// <summary>
        /// Give an item to another in game Character.
        /// </summary>
        /// <param name="item">The item to give.</param>
        /// <param name="character">The Character to give the item to.</param>
        /// <returns>True if the transaction completed OK, else false.</returns>
        public virtual bool Give(Item item, Character character)
        {
            if (!HasItem(item))
                return false;
            
            DequireItem(item);
            character.AcquireItem(item);
            return true;

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
            return InteractWithItem(item);
        }

        #endregion
    }
}