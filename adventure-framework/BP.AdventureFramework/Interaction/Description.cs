using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a description of an object.
    /// </summary>
    public class Description : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the description.
        /// </summary>
        protected string trueDescription;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Description class
        /// </summary>
        /// <param name="description">The description</param>
        public Description(string description)
        {
            trueDescription = description;
        }

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description as a string.</returns>
        public virtual string GetDescription()
        {
            return trueDescription;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Description.
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with.</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Description");
            writer.WriteAttributeString("trueDescription", GetDescription());
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Description.
        /// </summary>
        /// <param name="node">The node to read Xml from.</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            trueDescription = GetAttribute(node, "trueDescription").Value;
        }

        #endregion

        #endregion
    }
}