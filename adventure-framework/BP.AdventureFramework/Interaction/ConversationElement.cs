using System;
using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an element of a Conversation.
    /// </summary>
    public class ConversationElement : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the line.
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Get or set any action to carry out on this line.
        /// </summary>
        public Action Action { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ConversationElement class.
        /// </summary>
        public ConversationElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ConversationElement class.
        /// </summary>
        /// <param name="line">Specify the line in this conversation.</param>
        public ConversationElement(string line)
        {
            Line = line;
        }

        /// <summary>
        /// Initializes a new instance of the ConversationElement class.
        /// </summary>
        /// <param name="line">Specify the line in this conversation.</param>
        /// <param name="action">Specify any action to be carried out with this line.</param>
        public ConversationElement(string line, Action action)
        {
            Line = line;
            Action = action;
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ConversationElement.
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with.</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ConversationElement");
            writer.WriteAttributeString("Line", Line);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ConversationElement.
        /// </summary>
        /// <param name="node">The node to read Xml from.</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            Line = GetAttribute(node, "Line").Value;
        }

        #endregion

        #endregion
    }
}