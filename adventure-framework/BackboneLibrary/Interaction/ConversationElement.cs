using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an element of a Conversation
    /// </summary>
    public class ConversationElement : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the line
        /// </summary>
        public String Line
        {
            get { return this.line; }
            set { this.line = value; }
        }

        /// <summary>
        /// Get or set the line
        /// </summary>
        private String line;

        /// <summary>
        /// Get or set any action to carry out on this line
        /// </summary>
        public Action Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        /// <summary>
        /// Get or set any action to carry out on this line
        /// </summary>
        private Action action;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ConversationElement class
        /// </summary>
        public ConversationElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConversationElement class
        /// </summary>
        /// <param name="line">Specify the line in this conversation</param>
        public ConversationElement(String line)
        {
            // set line
            this.Line = line;
        }

        /// <summary>
        /// Initializes a new instance of the ConversationElement class
        /// </summary>
        /// <param name="line">Specify the line in this conversation</param>
        /// <param name="action">Specify any action to be carried out with this line</param>
        public ConversationElement(String line, Action action)
        {
            // set line
            this.Line = line;

            // set action
            this.Action = action;
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ConversationElement
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start
            writer.WriteStartElement("ConversationElement");

            // write attribute
            writer.WriteAttributeString("Line", this.Line);

            // write end
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Conversationelement
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get line
            this.Line = XMLSerializableObject.GetAttribute(node, "Line").Value;
        }

        #endregion

        #endregion
    }
}
