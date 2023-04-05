using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a Result to something
    /// </summary>
    public abstract class Result : XMLSerializableObject
    {
         #region Properies

        /// <summary>
        /// Get the descritpion of this Result
        /// </summary>
        public String Desciption
        {
            get { return this.desciption; }
            protected set { this.desciption = value; }
        }

        /// <summary>
        /// Get or set the descritpion of this Result
        /// </summary>
        private String desciption;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Result class
        /// </summary>
        protected Result()
        {
            // set description
            this.desciption = "There was no effect";
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this Result
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Result");

            // write description
            writer.WriteAttributeString("Description", this.Desciption);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Result
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // set description
            this.Desciption = XMLSerializableObject.GetAttribute(node, "Description").Value;
        }

        #endregion

        #endregion
    }
}
