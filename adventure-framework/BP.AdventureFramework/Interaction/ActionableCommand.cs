using System.Collections.Generic;
using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an actionable command.
    /// </summary>
    public class ActionableCommand : XMLSerializableObject, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the command.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Get or set the description of the command.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the action of the command.
        /// </summary>
        public ActionCallback Action { get; set; } = () => new InteractionResult(EInteractionEffect.NoEffect, "There was no effect");

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">A description of the command.</param>
        public ActionableCommand(string command, string description)
        {
            Command = command;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">A description of the command.</param>
        /// <param name="isPlayerVisible">Specify it this command is visible to the player.</param>
        public ActionableCommand(string command, string description, bool isPlayerVisible) : this(command, description)
        {
            IsPlayerVisible = isPlayerVisible;
        }

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">A description of the command.</param>
        /// <param name="isPlayerVisible">Specify it this command is visible to the player.</param>
        /// <param name="action">The action callback to the command.</param>
        public ActionableCommand(string command, string description, bool isPlayerVisible, ActionCallback action) : this(command, description, isPlayerVisible)
        {
            Action = action;
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ActionableCommand.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        protected virtual string OnGenerateTransferalID()
        {
            return Command + Description;
        }

        /// <summary>
        /// Handle transferal of delegation to this ActionableCommand from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            Action = ((ActionableCommand)source).Action;
        }

        /// <summary>
        /// Handle registration of all child properties of this ActionableCommand that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ActionableCommand.</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ActionableCommand.
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with.</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ActionableCommand");
            writer.WriteAttributeString("Command", Command);
            writer.WriteAttributeString("Description", Description);
            writer.WriteAttributeString("IsPlayerVisible", IsPlayerVisible.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ActionableCommand.
        /// </summary>
        /// <param name="node">The node to read Xml from.</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            Command = GetAttribute(node, "Command").Value;
            Description = GetAttribute(node, "Description").Value;
            IsPlayerVisible = bool.Parse(GetAttribute(node, "IsPlayerVisible").Value);
        }

        #endregion

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ActionableCommand.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        public string GenerateTransferalID()
        {
            return OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ActionableCommand from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ActionableCommand that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ActionableComand.</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }
}