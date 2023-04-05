using System.Xml;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a non-playable character
    /// </summary>
    public class NonPlayableCharacter : Character, ITalkative
    {
        #region ITalkative Members

        /// <summary>
        /// Talk to this object
        /// </summary>
        /// <returns>A string representing the conversation</returns>
        public string Talk()
        {
            return OnTalk();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the conversation
        /// </summary>
        public Conversation Conversation
        {
            get { return conversation; }
            set { conversation = value; }
        }

        /// <summary>
        /// Get or set the conversation
        /// </summary>
        private Conversation conversation;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        protected NonPlayableCharacter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        public NonPlayableCharacter(string name, string description)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        /// <param name="conversation">The conversation</param>
        public NonPlayableCharacter(string name, string description, Conversation conversation)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);

            // set conversation
            Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        public NonPlayableCharacter(string name, Description description)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        /// <param name="conversation">The conversation</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        /// <param name="conversation">The conversation</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            Conversation = conversation;

            // set if alive
            IsAlive = isAlive;

            // set interactoon
            Interaction = interaction;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        /// <param name="conversation">The conversation</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction</param>
        /// <param name="examination">Set this NonPlayableCharacter's examination</param>
        public NonPlayableCharacter(string name, Description description, Conversation conversation, bool isAlive, InteractionCallback interaction, ExaminationCallback examination)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            Conversation = conversation;

            // set if alive
            IsAlive = isAlive;

            // set interactoon
            Interaction = interaction;

            // set examination
            Examination = examination;
        }

        /// <summary>
        /// Handle talking
        /// </summary>
        /// <returns>A string representing the dialogue</returns>
        protected virtual string OnTalk()
        {
            // if there was, at least at some point some conversation
            if (Conversation != null)
            {
                // if some remaining lines
                if (Conversation.HasSomeRemainingLines ||
                    Conversation.RepeatLastElement)
                    // return next line
                    return Name + ": \"" + Conversation.NextLine() + "\"";
                return Name + " has nothing else to say";
            }

            // just return nothing to say
            return Name + " has nothing to say";
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this NonPlayableCharacter
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("NonPlayableCharacter");

            // if some converstaion
            if (Conversation != null)
                // write the conversation
                Conversation.WriteXml(writer);

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this NonPLayableCharacter
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // if a conversation node
            if (NodeExists(node, "Conversation"))
            {
                // if a null converstion
                if (Conversation == null)
                    // create new conversation
                    Conversation = new Conversation();

                // read conversation
                Conversation.ReadXmlNode(GetNode(node, "Conversation"));
            }

            // read base
            base.OnReadXmlNode(GetNode(node, "Character"));
        }

        #endregion

        #endregion
    }
}