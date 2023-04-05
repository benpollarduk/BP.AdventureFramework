using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using AdventureFramework.Locations;
using AdventureFramework.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an item that can be used within the game
    /// </summary>
    public class Item : ExaminableObject, IInteractWithItem, IImplementOwnActions
    {
        #region Properties

        /// <summary>
        /// Get or set if this is takeable
        /// </summary>
        public Boolean IsTakeable
        {
            get { return this.isTakeable; }
            protected set { this.isTakeable = value; }
        }

        /// <summary>
        /// Get or set if this is takeable
        /// </summary>
        private Boolean isTakeable = false;

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
        /// Get or set the morphic type of this item. This allows correct file IO for the type of this item if it has morphed into a new type
        /// </summary>
        protected String MorphicType
        {
            get { return this.morphicType ?? this.GetType().Name; }
            set { this.morphicType = value; }
        }

        /// <summary>
        /// Get or set the morphic type of this item. This allows correct file IO for the type of this item if it has morphed into a new type
        /// </summary>
        private String morphicType;

        /// <summary>
        /// Get or set the additional actionable commands
        /// </summary>
        private List<ActionableCommand> additionalCommands = new List<ActionableCommand>();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Item class
        /// </summary>
        protected Item()
        {
        }
                
        /// <summary>
        /// Initializes a new instance of the Item class
        /// </summary>
        /// <param name="name">The name of this Item</param>
        /// <param name="description">A description of this Item</param>
        /// <param name="isTakeable">Specify if this item is takeable</param>
        public Item(String name, String description, Boolean isTakeable)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);

            // set if takeable
            this.IsTakeable = isTakeable;
        }

        /// <summary>
        /// Initializes a new instance of the Item class
        /// </summary>
        /// <param name="name">The name of this Item</param>
        /// <param name="description">A description of this Item</param>
        /// <param name="isTakeable">Specify if this item is takeable</param>
        public Item(String name, Description description, Boolean isTakeable)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set if takeable
            this.IsTakeable = isTakeable;
        }

        /// <summary>
        /// Handle item morphing
        /// </summary>
        /// <param name="item">The item to morph into</param>
        public virtual void Morph(Item item)
        {
            // set name
            this.name = item.Name;

            // set description
            this.Description = item.Description;

            // set if visible
            this.isPlayerVisible = item.IsPlayerVisible;

            // set if takeable
            this.IsTakeable = item.IsTakeable;

            // set morphic type
            this.MorphicType = item.GetType().Name;
        }

        /// <summary>
        /// Use this item on a target
        /// </summary>
        /// <param name="target">The target to use the item on</param>
        /// <retunrs>The result of the interaction</retunrs>
        public virtual InteractionResult Use(IInteractWithItem target)
        {
            return target.Interact(this);
        }

        /// <summary>
        /// Handle interaction with other items
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        protected virtual InteractionResult OnInteract(Item item)
        {
            return this.interaction.Invoke(item, this);
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
        /// Handle transferal of delegation to this Item from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected override void OnTransferFrom(ITransferableDelegation source)
        {
            // get item
            Item i = source as Item;

            // set interaction
            this.Interaction = i.Interaction;
        }

        /// <summary>
        /// Handle registration of all child properties of this Item that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Item</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
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

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this Item
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Item");

            // write if takeable
            writer.WriteAttributeString("IsTakeable", this.IsTakeable.ToString());

            // write morphic type
            writer.WriteAttributeString("MorphicType", this.MorphicType);

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
        /// Handle reading of Xml for this Item
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // parse
            this.IsTakeable = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "IsTakeable").Value);

            // hold morphic type
            this.MorphicType = XMLSerializableObject.GetAttribute(node, "MorphicType").Value;

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

        #region IInteractWithItem Members

        /// <summary>
        /// Interact with an item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        public InteractionResult Interact(Item item)
        {
            return this.OnInteract(item);
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
            return this.ReactToAction(command);
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
