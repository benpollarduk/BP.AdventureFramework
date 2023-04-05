namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a playable character.
    /// </summary>
    public class PlayableCharacter : Character
    {
        #region Properties

        /// <summary>
        /// Occurs if this player dies.
        /// </summary>
        public event ReasonEventHandler Died;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="description">The description of the player.</param>
        public PlayableCharacter(string name, string description)
        {
            Name = name;
            Description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="description">The description of the player.</param>
        public PlayableCharacter(string name, Description description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(string name, string description, params Item[] items) : this(name, description)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="description">The description of the player.</param>
        /// <param name="items">The players items.</param>
        public PlayableCharacter(string name, Description description, params Item[] items) : this(name, description)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Use an item.
        /// </summary>
        /// <param name="targetObject">A target object to use the item on.</param>
        /// <param name="itemIndex">The index of the item to use.</param>
        /// <returns>The result of the items usage.</returns>
        public InteractionResult UseItem(IInteractWithItem targetObject, short itemIndex)
        {
            return UseItem(targetObject, Items[itemIndex]);
        }

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
        public override void Kill()
        {
            Kill(string.Empty);
        }

        /// <summary>
        /// Kill the character.
        /// </summary>
        /// <param name="reason">A reason for the death.</param>
        public override void Kill(string reason)
        {
            base.Kill(reason);
            Died?.Invoke(this, new ReasonEventArgs(reason));
        }

        #endregion
    }
}