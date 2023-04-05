using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a description of an object
    /// </summary>
    public class Description : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the description
        /// </summary>
        protected String trueDescription = String.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Description class
        /// </summary>
        protected Description()
        {
            // set description
            this.trueDescription = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the Description class
        /// </summary>
        /// <param name="description">The description</param>
        public Description(String description)
        {
            // set description
            this.trueDescription = description;
        }

        /// <summary>
        /// Get the descrpition
        /// </summary>
        /// <returns>The description as a string</returns>
        public virtual String GetDescription()
        {
            return this.trueDescription;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Description
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Description");

            // write attribute
            writer.WriteAttributeString("trueDescription", this.GetDescription());

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Description
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get attribute
            this.trueDescription = XMLSerializableObject.GetAttribute(node, "trueDescription").Value;
        }

        #endregion

        #endregion
    }
}
