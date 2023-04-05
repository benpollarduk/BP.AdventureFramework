using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a Result to something
    /// </summary>
    public abstract class Result : XMLSerializableObject
    {
        #region Properties

        /// <summary>
        /// Get the descritpion of this Result
        /// </summary>
        public string Desciption
        {
            get { return desciption; }
            protected set { desciption = value; }
        }

        /// <summary>
        /// Get or set the descritpion of this Result
        /// </summary>
        private string desciption;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Result class
        /// </summary>
        protected Result()
        {
            // set description
            desciption = "There was no effect";
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this Result
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Result");

            // write description
            writer.WriteAttributeString("Description", Desciption);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Result
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // set description
            Desciption = GetAttribute(node, "Description").Value;
        }

        #endregion

        #endregion
    }
}