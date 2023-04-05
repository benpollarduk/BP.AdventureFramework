using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using AdventureFramework.Locations;
using System.Xml.Serialization;
using AdventureFramework.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a generic in game character
    /// </summary>
    public abstract class Character : ExaminableObject, IInteractWithItem, IImplementOwnActions
    {
        #region Properties

        /// <summary>
        /// Get if this character is alive
        /// </summary>
        public Boolean IsAlive
        {
            get { return this.isAlive; }
            protected set { this.isAlive = value; }
        }

        /// <summary>
        /// Get or set if this character is alive
        /// </summary>
        private Boolean isAlive = true;

        /// <summary>
        /// Get or set the interaction
        /// </summary>
        public InteractionCallback Interaction
        {
            get { return this.interaction; }
            set { this.interaction = value; }
        }

        /// <summary>
        /// Get or set the interaction
        /// </summary>
        private InteractionCallback interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
        {
            // return default
            return new InteractionResult(EInteractionEffect.NoEffect, i);
        });

        /// <summary>
        /// Get or set the additional actionable commands
        /// </summary>
        private List<ActionableCommand> additionalCommands = new List<ActionableCommand>();

        /// <summary>
        /// Get the items this Character holds
        /// </summary>
        public Item[] Items
        {
            get { return this.items.ToArray<Item>(); }
            protected set { this.items = new List<Item>(value); }
        }

        /// <summary>
        /// Get or set the items this Character holds
        /// </summary>
        protected List<Item> items = new List<Item>();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Character class
        /// </summary>
        protected Character()
        {
        }

        /// <summary>
        /// Interact with a specified item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        protected virtual InteractionResult InteractWithItem(Item item)
        {
            // handle interaction
            return this.interaction.Invoke(item, this);
        }

        /// <summary>
        /// Kill this character
        /// </summary>
        public virtual void Kill()
        {
            // dead
            this.isAlive = false;
        }

        /// <summary>
        /// Kill the character
        /// </summary>
        /// <param name="reason">A reason for the death</param>
        public virtual void Kill(String reason)
        {
            // dead
            this.isAlive = false;
        }

        /// <summary>
        /// Handle reactions to ActionableCommands
        /// </summary>
        /// <param name="command">The command to react to</param>
        /// <returns>The result of the command</returns>
        protected virtual InteractionResult OnReactToAction(ActionableCommand command)
        {
            // if command is found
            if (this.AdditionalCommands.Contains(command))
            {
                // invoke action
                return command.Action.Invoke();
            }
            else
            {
                // throw exception
                throw new ArgumentException(String.Format("Command {0} was not found on object {1}", command.Command, this.Name));
            }
        }

        /// <summary>
        /// Handle transferal of delegation to this Character from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected override void OnTransferFrom(ITransferableDelegation source)
        {
            // get character
            Character c = source as Character;

            // set interaction
            this.Interaction = c.Interaction;
        }

        /// <summary>
        /// Handle registration of all child properties of this Character that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Character</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // itterate all items
            foreach (Item i in this.Items)
            {
                // add
                children.Add(i);

                // register children
                i.RegisterTransferableChildren(ref children);
            }

            // itterate commands
            foreach (ActionableCommand command in this.AdditionalCommands)
            {
                // add command
                children.Add(command);

                // register children
                command.RegisterTransferableChildren(ref children);
            }

            base.OnRegisterTransferableChildren(ref children);
        }

        /// <summary>
        /// Aquire an item
        /// </summary>
        /// <param name="item">The item to aquire</param>
        public virtual void AquireItem(Item item)
        {
            // add the item
            this.items.Add(item);
        }

        /// <summary>
        /// Dequire an item
        /// </summary>
        /// <param name="item">The item to dequire</param>
        public virtual void DequireItem(Item item)
        {
            // add the item
            this.items.Remove(item);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="item">The item</param>
        /// <returns>True if the item is found, else false</returns>
        public virtual Boolean HasItem(Item item)
        {
            // check if found
            return this.HasItem(item, false);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item is found, else false</returns>
        public virtual Boolean HasItem(Item item, Boolean includeInvisibleItems)
        {
            // check if found
            return this.Items.Contains<Item>(item) && ((includeInvisibleItems) || (item.IsPlayerVisible));
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <returns>True if the item is found, else false</returns>
        public virtual Boolean HasItem(String itemName)
        {
            // hold item
            Item i;

            // check if found
            return this.FindItem(itemName, out i, false);
        }

        /// <summary>
        /// Determine if this PlayableCharacter has an item
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item is found, else false</returns>
        public virtual Boolean HasItem(String itemName, Boolean includeInvisibleItems)
        {
            // hold item
            Item i;

            // check if found
            return this.FindItem(itemName, out i, includeInvisibleItems);
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found</returns>
        public virtual Boolean FindItem(String itemName, out Item item)
        {
            return this.FindItem(itemName, out item, false);
        }

        /// <summary>
        /// Find an item
        /// </summary>
        /// <param name="itemName">The items name. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item was found</returns>
        public virtual Boolean FindItem(String itemName, out Item item, Boolean includeInvisibleItems)
        {
            // hold items
            Item[] items = this.Items.Where<Item>((Item x) => ((x.Name.ToUpper() == itemName.ToUpper()) && ((includeInvisibleItems) || (x.IsPlayerVisible)))).ToArray<Item>();

            // if found
            if (items.Length > 0)
            {
                // set item
                item = items[0];

                // item found
                return true;
            }
            else
            {
                // no item
                item = null;

                // fail
                return false;
            }
        }

        /// <summary>
        /// Find an item. This will not include items whose ExaminableObject.IsPlayerVisible property is set to false
        /// </summary>
        /// <param name="itemID">The items ID. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <returns>True if the item was found</returns>
        internal virtual Boolean FindItemByID(String itemID, out Item item)
        {
            return this.FindItemByID(itemID, out item, false);
        }

        /// <summary>
        /// Find an item
        /// </summary>
        /// <param name="itemID">The items ID. This is case insensitive</param>
        /// <param name="item">The item</param>
        /// <param name="includeInvisibleItems">Specify if invisible items should be included</param>
        /// <returns>True if the item was found</returns>
        internal virtual Boolean FindItemByID(String itemID, out Item item, Boolean includeInvisibleItems)
        {
            // hold items
            Item[] items = this.Items.Where<Item>((Item x) => ((x.ID == itemID) && ((includeInvisibleItems) || (x.IsPlayerVisible)))).ToArray<Item>();

            // if found
            if (items.Length > 0)
            {
                // set item
                item = items[0];

                // item found
                return true;
            }
            else
            {
                // no item
                item = null;

                // fail
                return false;
            }
        }

        /// <summary>
        /// Get items as a list
        /// </summary>
        /// <returns>A list of all</returns>
        internal virtual String GetItemsAsList()
        {
            // if some items
            if (this.Items.Length > 0)
            {
                // hold entrace
                String itemsInRoom = String.Empty;

                // hold item names
                List<String> itemNames = new List<String>();

                // itterate all items
                foreach (Item i in this.Items)
                {
                    // if visible
                    if (i.IsPlayerVisible)
                    {
                        // add name
                        itemNames.Add(i.Name);
                    }
                }

                // sort
                itemNames.Sort();

                // itterate names
                foreach (String name in itemNames)
                {
                    // add item
                    itemsInRoom += name + ", ";
                }

                // remove last ", "
                itemsInRoom = itemsInRoom.Remove(itemsInRoom.Length - 2);

                // return items
                return itemsInRoom;
            }
            else
            {
                // none
                return String.Empty;
            }
        }

        /// <summary>
        /// Get all IImplementOwnActions objects within this Character
        /// </summary>
        /// <returns>An array of all IImplementOwnActions objects within this Character</returns>
        public virtual IImplementOwnActions[] GetAllObjectsWithAdditionalCommands()
        {
            // hold custom commandables
            List<IImplementOwnActions> customCommands = new List<IImplementOwnActions>();

            // add items
            customCommands.AddRange(this.Items.Where<Item>((Item i) => i.IsPlayerVisible).ToArray<Item>());

            // add player if commandable
            customCommands.Add(this);

            // return as array
            return customCommands.ToArray<IImplementOwnActions>();
        }

        /// <summary>
        /// Give an item to another in game character
        /// </summary>
        /// <param name="item">The item to give</param>
        /// <param name="character">The character to give the item to</param>
        /// <returns>True if the transaction completed OK, else false</returns>
        public virtual Boolean Give(Item item, Character character)
        {
            // if this chartacter has the item
            if (this.HasItem(item, true))
            {
                // dequire
                this.DequireItem(item);

                // aquire
                character.AquireItem(item);

                // pass
                return true;
            }
            else
            {
                // fail
                return false;
            }
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Character
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Character");
            
            // write is alive property
            writer.WriteAttributeString("IsAlive", this.IsAlive.ToString());

            // write items
            writer.WriteStartElement("Items");

            // itterate items
            for (Int32 i = 0; i < this.Items.Length; i++)
            {
                // add item
                this.Items[i].WriteXml(writer);
            }

            // write end of items
            writer.WriteEndElement();

            // write start element
            writer.WriteStartElement("AdditionalActionableCommands");

            // itterate all custom commands
            foreach (ActionableCommand command in this.AdditionalCommands)
            {
                // write
                command.WriteXml(writer);
            }

            // write end element
            writer.WriteEndElement();

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Character
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get items node
            XmlNode itemsNode = XMLSerializableObject.GetNode(node, "Items");

            // hold all item ID's that are in collection
            List<String> itemIdsInCollection = new List<String>();

            // read items
            for (Int32 index = 0; index < itemsNode.ChildNodes.Count; index++)
            {
                // get node for element
                XmlNode itemElementNode = itemsNode.ChildNodes[index];

                // find examinable object node
                XmlNode examinableObjectNode = XMLSerializableObject.GetNode(itemElementNode, "ExaminableObject");

                // add to collection
                itemIdsInCollection.Add(XMLSerializableObject.GetAttribute(examinableObjectNode, "ID").Value);

                // hold item
                Item item = null;

                // if item is not found
                if (!this.FindItemByID(XMLSerializableObject.GetAttribute(examinableObjectNode, "ID").Value, out item, true))
                {
                    // create new item
                    item = new Item(String.Empty, String.Empty, false);

                    // add item
                    this.items.Add(item);
                }

                // read node
                item.ReadXmlNode(itemElementNode);
            }

            // hold all items to remove
            List<Item> itemsToRemove = new List<Item>();

            // itterate each collectable in posession
            foreach (Item i in this.items)
            {
                // if not found in loaded items
                if (!itemIdsInCollection.Contains(i.ID))
                {
                    // hold item
                    itemsToRemove.Add(i);
                }
            }

            // remove if not in list
            this.items.RemoveAll((Item i) => itemsToRemove.Contains(i));

            // get if alive
            this.IsAlive = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "IsAlive").Value);

            // get custom commands node
            XmlNode customCommandsNode = XMLSerializableObject.GetNode(node, "AdditionalActionableCommands");

            // itterate all child nodes
            for (Int32 index = 0; index < customCommandsNode.ChildNodes.Count; index++)
            {
                // read from node
                this.AdditionalCommands[index].ReadXmlNode(customCommandsNode.ChildNodes[index]);
            }

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "ExaminableObject"));
        }

        #endregion

        #endregion

        #region IInteractWithInGameItem Members

        /// <summary>
        /// Interact with an item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        public InteractionResult Interact(Item item)
        {
            return this.InteractWithItem(item);
        }

        #endregion

        #region IImplementOwnActions Members

        /// <summary>
        /// Get or set the ActionableCommands this object can interact with
        /// </summary>
        public List<ActionableCommand> AdditionalCommands
        {
            get
            {
                return this.additionalCommands;
            }
            set
            {
                this.additionalCommands = value;
            }
        }

        /// <summary>
        /// React to an ActionableCommand
        /// </summary>
        /// <param name="command">The command to react to</param>
        /// <returns>The result of the interaction</returns>
        public InteractionResult ReactToAction(ActionableCommand command)
        {
            return this.OnReactToAction(command);
        }

        /// <summary>
        /// Find a command by it's name
        /// </summary>
        /// <param name="command">The name of the command to find</param>
        /// <returns>The ActionableCommand (if it is found)</returns>
        public ActionableCommand FindCommand(string command)
        {
            // itterate all commands
            foreach (ActionableCommand c in this.AdditionalCommands)
            {
                // check commands
                if (c.Command.ToUpper() == command.ToUpper())
                {
                    // found
                    return c;
                }
            }

            // not found
            return null;
        }

        #endregion
    }
}
