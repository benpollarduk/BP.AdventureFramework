using System;
using System.Collections.Generic;
using System.Linq;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an item that can be used within the game.
    /// </summary>
    public class Item : ExaminableObject, IInteractWithItem, IImplementOwnActions
    {
        #region Fields

        private string morphicType;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if this is takeable.
        /// </summary>
        public bool IsTakeable { get; protected set; }

        /// <summary>
        /// Get or set the interaction.
        /// </summary>
        public InteractionCallback Interaction { get; set; } = (i, target) => new InteractionResult(InteractionEffect.NoEffect, i);

        /// <summary>
        /// Get or set the morphic type of this item. This allows correct file IO for the type of this item if it has morphed into a new type.
        /// </summary>
        protected string MorphicType
        {
            get { return morphicType ?? GetType().Name; }
            set { morphicType = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="identifier">This Items identifier.</param>
        /// <param name="description">A description of this Item.</param>
        /// <param name="isTakeable">Specify if this item is takeable.</param>
        public Item(Identifier identifier, string description, bool isTakeable)
        {
            Identifier = identifier;
            Description = new Description(description);
            IsTakeable = isTakeable;
        }

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
        public virtual void Morph(Item item)
        {
            Identifier = item.Identifier;
            Description = item.Description;
            IsPlayerVisible = item.IsPlayerVisible;
            IsTakeable = item.IsTakeable;
            MorphicType = item.GetType().Name;
        }

        /// <summary>
        /// Use this item on a target.
        /// </summary>
        /// <param name="target">The target to use the item on.</param>
        /// <retunrs>The result of the interaction.</retunrs>
        public virtual InteractionResult Use(IInteractWithItem target)
        {
            return target.Interact(this);
        }

        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Transfer delegation to this ExaminableObject from a source ITransferableDelegation object.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public override void TransferFrom(ITransferableDelegation source)
        {
            var i = source as Item;
            Interaction = i?.Interaction;
        }

        /// <summary>
        /// Register all child properties of this ExaminableObject that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject.</param>
        public override void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            foreach (var command in AdditionalCommands)
            {
                children.Add(command);
                command.RegisterTransferableChildren(ref children);
            }

            base.RegisterTransferableChildren(ref children);
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
        public virtual InteractionResult ReactToAction(ActionableCommand command)
        {
            if (AdditionalCommands.Contains(command))
                return command.Action.Invoke();

            throw new ArgumentException($"Command {command.Command} was not found on object {Identifier}");
        }

        /// <summary>
        /// Find a command by it's name.
        /// </summary>
        /// <param name="command">The name of the command to find.</param>
        /// <returns>The ActionableCommand.</returns>
        public ActionableCommand FindCommand(string command)
        {
            return AdditionalCommands.FirstOrDefault(c => string.Equals(c.Command, command, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion

        #region IInteractWithItem Members

        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        public virtual InteractionResult Interact(Item item)
        {
            return Interaction.Invoke(item, this);
        }

        #endregion
    }
}