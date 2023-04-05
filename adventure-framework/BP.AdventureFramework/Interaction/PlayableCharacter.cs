using System.Xml;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a playable character
    /// </summary>
    public class PlayableCharacter : Character
    {
        #region Properties

        /// <summary>
        /// Occurs if this player dies
        /// </summary>
        public event ReasonEventHandler Died;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class
        /// </summary>
        protected PlayableCharacter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="description">The description of the player</param>
        public PlayableCharacter(string name, string description)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="description">The description of the player</param>
        public PlayableCharacter(string name, Description description)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="description">The description of the player</param>
        /// <param name="items">The players items</param>
        public PlayableCharacter(string name, string description, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);

            // add items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the PlayableCharacter class
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="description">The description of the player</param>
        /// <param name="items">The players items</param>
        public PlayableCharacter(string name, Description description, params Item[] items)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // add items
            this.items.AddRange(items);
        }

        /// <summary>
        /// Use an item
        /// </summary>
        /// <param name="targetObject">A target object to use the item on</param>
        /// <param name="itemIndex">The index of the item to use</param>
        /// <returns>The result of the items usage</returns>
        public InteractionResult UseItem(IInteractWithItem targetObject, short itemIndex)
        {
            // use the item
            return UseItem(targetObject, Items[itemIndex]);
        }

        /// <summary>
        /// Use an item
        /// </summary>
        /// <param name="targetObject">A target object to use the item on</param>
        /// <param name="item">The item to use</param>
        /// <returns>The result of the items usage</returns>
        public InteractionResult UseItem(IInteractWithItem targetObject, Item item)
        {
            // use the item
            var result = targetObject.Interact(item);

            // if fatal
            if (result.Effect == EInteractionEffect.FatalEffect)
                // died
                IsAlive = false;

            // return the result
            return result;
        }

        /// <summary>
        /// Kill the character
        /// </summary>
        public override void Kill()
        {
            Kill(string.Empty);
        }

        /// <summary>
        /// Kill the character
        /// </summary>
        /// <param name="reason">A reason for the death</param>
        public override void Kill(string reason)
        {
            base.Kill(reason);

            // if subscribers
            if (Died != null)
                // died
                Died(this, new ReasonEventArgs(reason));
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this PlayableCharacter
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("PlayableCharacter");

            // write base
            base.OnWriteXml(writer);

            // write end of player
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this PlayableCharacter
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // read base node
            base.OnReadXmlNode(GetNode(node, "Character"));
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Enumeration of commands
    /// </summary>
    public enum ECommand
    {
        /// <summary>
        /// Use an item
        /// </summary>
        Use = 0,

        /// <summary>
        /// Take an item
        /// </summary>
        Take,

        /// <summary>
        /// Examine something
        /// </summary>
        Examine,

        /// <summary>
        /// Drop an item
        /// </summary>
        Drop,

        /// <summary>
        /// Talk to a non-playable character
        /// </summary>
        Talk
    }
}