using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a conditional description of an object
    /// </summary>
    public class ConditionalDescription : Description, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the description for when this condition is false
        /// </summary>
        private String falseDescription = String.Empty;

        /// <summary>
        /// Get or set the condition
        /// </summary>
        public Condition Condition
        {
            get { return this.condition; }
            set { this.condition = value; }
        }

        /// <summary>
        /// Get or set the condition
        /// </summary>
        private Condition condition = null;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes anew instance of the ConditionalDescription class
        /// </summary>
        protected ConditionalDescription()
        {
        } 

        /// <summary>
        /// Initializes anew instance of the ConditionalDescription class
        /// </summary>
        /// <param name="trueDescription">The true description</param>
        /// <param name="falseDescription">The false description</param>
        /// <param name="condition">The condition</param>
        public ConditionalDescription(String trueDescription, String falseDescription, Condition condition)
        {
            // set true
            this.trueDescription = trueDescription;

            // set false
            this.falseDescription = falseDescription;

            // set condition
            this.Condition = condition;
        }

        /// <summary>
        /// Get the description
        /// </summary>
        /// <returns>The description as a string</returns>
        public override String GetDescription()
        {
            // if a condition
            if (this.Condition != null)
            {
                // return description
                return this.Condition.Invoke() ? this.trueDescription : this.falseDescription;
            }
            else
            {
                // return description
                return this.trueDescription;
            }
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ConditionalDescription
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        protected virtual String OnGenerateTransferalID()
        {
            return this.trueDescription + this.falseDescription;
        }

        /// <summary>
        /// Handle transferal of delegation to this ConditionalDescription from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            // set condition
            this.Condition = ((ConditionalDescription)source).Condition;
        }

        /// <summary>
        /// Handle registration of all child properties of this ConditionalDescription that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ConditionalDescription</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ConditionalDescription
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write description
            writer.WriteStartElement("ConditionalDescription");

            // write attribute
            writer.WriteAttributeString("falseDescription", this.GetDescription());
            
            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ConditionalDescription
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get false description
            this.falseDescription = XMLSerializableObject.GetAttribute(node, "falseDescription").Value;

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "Description"));
        }

        #endregion

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ConditionalDescription
        /// </summary>
        /// <returns>The ID as a string</returns>
        public string GenerateTransferalID()
        {
            return this.OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ConditionalDescription from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            this.OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ConditionalDescription that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ConditionalDescription</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            this.OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }

    /// <summary>
    /// Represents a callback for conditions
    /// </summary>
    /// <returns>The result of the condition</returns>
    public delegate Boolean Condition();
}
