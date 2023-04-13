using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Characters
{
    /// <summary>
    /// Represents a generic in game character.
    /// </summary>
    public abstract class Character : ExaminableObject, IInteractWithItem, IImplementOwnActions
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
        public List<Item> Items { get; protected set; } = new List<Item>();

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
        /// Kill this character.
        /// </summary>
        public virtual void Kill()
        {
            IsAlive = false;
        }

        /// <summary>
        /// Kill the character.
        /// </summary>
        /// <param name="reason">A reason for the death.</param>
        public virtual void Kill(string reason)
        {
            IsAlive = false;
        }

        /// <summary>
        /// Handle reactions to ActionableCommands.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The result of the command.</returns>
        protected virtual InteractionResult OnReactToAction(ActionableCommand command)
        {
            if (AdditionalCommands.Contains(command))
                return command.Action.Invoke();

            throw new ArgumentException($"Command {command.Command} was not found on object {Identifier}");
        }

        /// <summary>
        /// Acquire an item.
        /// </summary>
        /// <param name="item">The item to acquire.</param>
        public virtual void AquireItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// De-acquire an item.
        /// </summary>
        /// <param name="item">The item to de-acquire.</param>
        public virtual void DequireItem(Item item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(Item item)
        {
            return HasItem(item, false);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(Item item, bool includeInvisibleItems)
        {
            return Items.Contains(item) && (includeInvisibleItems || item.IsPlayerVisible);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(string itemName)
        {
            return FindItem(itemName, out _, false);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item.
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item is found, else false.</returns>
        public virtual bool HasItem(string itemName, bool includeInvisibleItems)
        {
            return FindItem(itemName, out _, includeInvisibleItems);
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false.
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive.</param>
        /// <param name="item">The item.</param>
        /// <returns>True if the item was found.</returns>
        public virtual bool FindItem(string itemName, out Item item)
        {
            return FindItem(itemName, out item, false);
        }

        /// <summary>
        /// Find an item.
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive.</param>
        /// <param name="item">The item.</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included.</param>
        /// <returns>True if the item was found.</returns>
        public virtual bool FindItem(string itemName, out Item item, bool includeInvisibleItems)
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
        /// Get items as a list.
        /// </summary>
        /// <returns>A list of all.</returns>
        internal virtual string GetItemsAsList()
        {
            if (!Items.Any()) 
                return string.Empty;

            var itemsInRoom = string.Empty;
            var itemNames = (from i in Items where i.IsPlayerVisible select i.Identifier).ToList();

            itemNames.Sort();

            foreach (var n in itemNames)
                itemsInRoom += n + ", ";

            return itemsInRoom.Remove(itemsInRoom.Length - 2);
        }

        /// <summary>
        /// Get all IImplementOwnActions objects within this Character.
        /// </summary>
        /// <returns>An array of all IImplementOwnActions objects within this Character.</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            var customCommands = new List<IImplementOwnActions>();
            customCommands.AddRange(Items.Where(i => i.IsPlayerVisible).ToArray());
            customCommands.Add(this);
            return customCommands.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Give an item to another in game Character.
        /// </summary>
        /// <param name="item">The item to give.</param>
        /// <param name="character">The Character to give the item to.</param>
        /// <returns>True if the transaction completed OK, else false.</returns>
        public virtual bool Give(Item item, Character character)
        {
            if (!HasItem(item, true))
                return false;
            
            DequireItem(item);
            character.AquireItem(item);
            return true;

        }

        #endregion

        #region IImplementOwnActions Members

        /// <summary>
        /// Get or set the ActionableCommands this object can interact with.
        /// </summary>
        public List<ActionableCommand> AdditionalCommands { get; set; } = new List<ActionableCommand>();

        /// <summary>
        /// React to an ActionableCommand.
        /// </summary>
        /// <param name="command">The command to react to.</param>
        /// <returns>The result of the interaction.</returns>
        public InteractionResult ReactToAction(ActionableCommand command)
        {
            return OnReactToAction(command);
        }

        /// <summary>
        /// Find a command by its name.
        /// </summary>
        /// <param name="command">The name of the command to find.</param>
        /// <returns>The ActionableCommand.</returns>
        public ActionableCommand FindCommand(string command)
        {
            foreach (var c in AdditionalCommands)
            {
                if (string.Equals(c.Command, command, StringComparison.CurrentCultureIgnoreCase))
                    return c;
            }

            return null;
        }

        #endregion

        #region IInteractWithInGameItem Members

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