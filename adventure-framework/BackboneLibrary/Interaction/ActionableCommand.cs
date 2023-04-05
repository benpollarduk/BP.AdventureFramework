using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.IO;
using AdventureFramework.Rendering;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an actionable command
    /// </summary>
    public class ActionableCommand : XMLSerializableObject, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the command
        /// </summary>
        public String Command
        {
            get { return this.command; }
            set { this.command = value; }
        }

        /// <summary>
        /// Get or set the custom command
        /// </summary>
        private String command = String.Empty;

        /// <summary>
        /// Get or set the description of the command
        /// </summary>
        public String Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Get or set the description of the command
        /// </summary>
        private String description = String.Empty;

        /// <summary>
        /// Get or set the action of the command
        /// </summary>
        public ActionCallback Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        /// <summary>
        /// Get or set the action of the command
        /// </summary>
        private ActionCallback action = new ActionCallback(() =>
        {
            // no effect
            return new InteractionResult(EInteractionEffect.NoEffect, "There was no effect");
        });

        /// <summary>
        /// Get or set if this is visible to the player
        /// </summary>
        public Boolean IsPlayerVisible
        {
            get { return this.isPlayerVisible; }
            set { this.isPlayerVisible = value; }
        }

        /// <summary>
        /// Get or set if this is visible to the player
        /// </summary>
        protected Boolean isPlayerVisible = true;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class
        /// </summary>
        public ActionableCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="description">A description of the command</param>
        public ActionableCommand(String command, String description)
        {
            // set command
            this.Command = command;

            // set description
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="description">A description of the command</param>
        /// <param name="isPlayerVisible">Specify it this command is visible to the player</param>
        public ActionableCommand(String command, String description, Boolean isPlayerVisible)
        {
            // set command
            this.Command = command;

            // set description
            this.Description = description;

            // set if visible
            this.IsPlayerVisible = isPlayerVisible;
        }

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="description">A description of the command</param>
        /// <param name="isPlayerVisible">Specify it this command is visible to the player</param>
        /// <param name="action">The action callback to the command</param>
        public ActionableCommand(String command, String description, Boolean isPlayerVisible, ActionCallback action)
        {
            // set command
            this.Command = command;

            // set description
            this.Description = description;

            // set if visible
            this.IsPlayerVisible = isPlayerVisible;

            // set action
            this.Action = action;
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ActionableCommand
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        protected virtual String OnGenerateTransferalID()
        {
            return this.command.ToString() + this.description.ToString();
        }

        /// <summary>
        /// Handle transferal of delegation to this ActionableCommand from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            // set action
            this.Action = ((ActionableCommand)source).Action;
        }

        /// <summary>
        /// Handle registration of all child properties of this ActionableCommand that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ActionableCommand</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ActionableCommand
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("ActionableCommand");

            // write command
            writer.WriteAttributeString("Command", this.Command);

            // write description
            writer.WriteAttributeString("Description", this.Description);

            // write description
            writer.WriteAttributeString("IsPlayerVisible", this.IsPlayerVisible.ToString()); 

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ActionableCommand
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get command
            this.Command = XMLSerializableObject.GetAttribute(node, "Command").Value;

            // get description
            this.Description = XMLSerializableObject.GetAttribute(node, "Description").Value;

            // get if player visible
            this.IsPlayerVisible = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "IsPlayerVisible").Value);
        }

        #endregion

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ActionableCommand
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        public string GenerateTransferalID()
        {
            return this.OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ActionableCommand from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            this.OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ActionableCommand that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ActionableComand</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            this.OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }

    /// <summary>
    /// Represents the callback for a action
    /// </summary>
    /// <returns>The result of the interaction</returns>
    public delegate InteractionResult ActionCallback();
}
