using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an actionable command.
    /// </summary>
    public class ActionableCommand : ITransferableDelegation
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
        public ActionCallback Action { get; set; } = () => new InteractionResult(InteractionEffect.NoEffect, "There was no effect");

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion

        #region Constructors

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

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ActionableCommand.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        public virtual string GenerateTransferalID()
        {
            return Command + Description;
        }

        /// <summary>
        /// Transfer delegation to this ActionableCommand from a source ITransferableDelegation object.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public virtual void TransferFrom(ITransferableDelegation source)
        {
            Action = ((ActionableCommand)source).Action;
        }

        /// <summary>
        /// Register all child properties of this ActionableCommand that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ActionableComand.</param>
        public virtual void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #endregion
    }
}