using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents the result of an examination
    /// </summary>
    public class ExaminationResult : Result
    {
        #region Properties

        /// <summary>
        /// Get the type of result
        /// </summary>
        public EExaminationResults Type
        {
            get { return this.type; }
            protected set { this.type = value; }
        }

        /// <summary>
        /// Get or set the type of result
        /// </summary>
        public EExaminationResults type = EExaminationResults.DescriptionReturned;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class
        /// </summary>
        protected ExaminationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class
        /// </summary>
        /// <param name="description">A description of the result</param>
        public ExaminationResult(String description)
        {
            // set description
            this.Desciption = description;

            // set type
            this.Type = EExaminationResults.DescriptionReturned;
        }

        /// <summary>
        /// Initializes a new instance of the ExaminationResult class
        /// </summary>
        /// <param name="description">A description of the result</param>
        /// <param name="type">The type of this result</param>
        public ExaminationResult(String description, EExaminationResults type)
        {
            // set description
            this.Desciption = description;

            // set type
            this.Type = type;
        }

        #region XMLSeralization

        /// <summary>
        /// Handle writing of Xml for this ExaminationResult
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("ExaminationResult");

            // write attribute
            writer.WriteAttributeString("Type", this.Type.ToString());

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ExaminationResult
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // read type
            this.Type = (EExaminationResults)Enum.Parse(typeof(ExaminationResult), XMLSerializableObject.GetAttribute(node, "Type").Value);

            // set description
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "Result"));
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Enumeration of interaction effects
    /// </summary>
    public enum EExaminationResults
    {
        /// <summary>
        /// No effect to the interaction on either the item or the target
        /// </summary>
        DescriptionReturned = 0,
        /// <summary>
        /// Any other self contained effect
        /// </summary>
        SelfContained
    }
}
