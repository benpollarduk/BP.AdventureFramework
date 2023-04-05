using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using AdventureFramework.Locations;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a non-playable character
    /// </summary>
    public class NonPlayableCharacter : Character, ITalkative
    {
        #region Properties

        /// <summary>
        /// Get or set the conversation
        /// </summary>
        public Conversation Conversation
        {
            get { return this.conversation; }
            set { this.conversation = value; }
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
        public NonPlayableCharacter(String name, String description)
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
        public NonPlayableCharacter(String name, String description, Conversation conversation)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);

            // set conversation
            this.Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        public NonPlayableCharacter(String name, Description description)
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
        public NonPlayableCharacter(String name, Description description, Conversation conversation)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            this.Conversation = conversation;
        }

        /// <summary>
        /// Initializes a new instance of the NonPlayableCharacter class
        /// </summary>
        /// <param name="name">The name of this NonPlayableCharacter</param>
        /// <param name="description">The description of this NonPlayableCharacter</param>
        /// <param name="conversation">The conversation</param>
        /// <param name="isAlive">Set if this NonPlayableCharacter is alive</param>
        /// <param name="interaction">Set this NonPlayableCharacter's interaction</param>
        public NonPlayableCharacter(String name, Description description, Conversation conversation, Boolean isAlive, InteractionCallback interaction)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            this.Conversation = conversation;

            // set if alive
            this.IsAlive = isAlive;

            // set interactoon
            this.Interaction = interaction;
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
        public NonPlayableCharacter(String name, Description description, Conversation conversation, Boolean isAlive, InteractionCallback interaction, ExaminationCallback examination)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;

            // set conversation
            this.Conversation = conversation;

            // set if alive
            this.IsAlive = isAlive;

            // set interactoon
            this.Interaction = interaction;

            // set examination
            this.Examination = examination;
        }

        /// <summary>
        /// Handle talking
        /// </summary>
        /// <returns>A string representing the dialogue</returns>
        protected virtual String OnTalk()
        {
            // if there was, at least at some point some conversation
            if (this.Conversation != null)
            {
                // if some remaining lines
                if ((this.Conversation.HasSomeRemainingLines) ||
                    (this.Conversation.RepeatLastElement))
                {
                    // return next line
                    return this.Name + ": \"" + this.Conversation.NextLine() + "\"";
                }
                else
                {
                    // just return nothing else to say
                    return this.Name + " has nothing else to say";
                }
            }
            else
            {
                // just return nothing to say
                return this.Name + " has nothing to say";
            }
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this NonPlayableCharacter
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("NonPlayableCharacter");

            // if some converstaion
            if (this.Conversation != null)
            {
                // write the conversation
                this.Conversation.WriteXml(writer);
            }

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this NonPLayableCharacter
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // if a conversation node
            if (XMLSerializableObject.NodeExists(node, "Conversation"))
            {
                // if a null converstion
                if (this.Conversation == null)
                {
                    // create new conversation
                    this.Conversation = new Conversation();
                }

                // read conversation
                this.Conversation.ReadXmlNode(XMLSerializableObject.GetNode(node, "Conversation"));
            }

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "Character"));
        }

        #endregion

        #endregion

        #region ITalkative Members

        /// <summary>
        /// Talk to this object
        /// </summary>
        /// <returns>A string representing the conversation</returns>
        public string Talk()
        {
            return this.OnTalk();
        }

        #endregion
    }
}
