using System.Collections.Generic;
using System.Xml;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents a conditional description of an object.
    /// </summary>
    public class ConditionalDescription : Description, ITransferableDelegation
    {
        #region Properties

        /// <summary>
        /// Get or set the description for when this condition is false
        /// </summary>
        private string falseDescription;

        /// <summary>
        /// Get or set the condition
        /// </summary>
        public Condition Condition { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes anew instance of the ConditionalDescription class.
        /// </summary>
        /// <param name="trueDescription">The true description.</param>
        /// <param name="falseDescription">The false description.</param>
        /// <param name="condition">The condition.</param>
        public ConditionalDescription(string trueDescription, string falseDescription, Condition condition)
        {
            this.trueDescription = trueDescription;
            this.falseDescription = falseDescription;
            Condition = condition;
        }

        /// <summary>
        /// Get the description.
        /// </summary>
        /// <returns>The description as a string.</returns>
        public override string GetDescription()
        {
            if (Condition != null)
                return Condition.Invoke() ? trueDescription : falseDescription;

            return trueDescription;
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ConditionalDescription.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        protected virtual string OnGenerateTransferalID()
        {
            return trueDescription + falseDescription;
        }

        /// <summary>
        /// Handle transferal of delegation to this ConditionalDescription from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            Condition = ((ConditionalDescription)source).Condition;
        }

        /// <summary>
        /// Handle registration of all child properties of this ConditionalDescription that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ConditionalDescription.</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // no children to register
        }

        #region XMLSerialization

        /// <summary>
        /// Handle writing of Xml for this ConditionalDescription.
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with.</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ConditionalDescription");
            writer.WriteAttributeString("falseDescription", GetDescription());
            base.OnWriteXml(writer);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ConditionalDescription.
        /// </summary>
        /// <param name="node">The node to read Xml from.</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            falseDescription = GetAttribute(node, "falseDescription").Value;
            base.OnReadXmlNode(GetNode(node, "Description"));
        }

        #endregion

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ConditionalDescription.
        /// </summary>
        /// <returns>The ID as a string.</returns>
        public string GenerateTransferalID()
        {
            return OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ConditionalDescription from a source ITransferableDelegation object. This should only concern top level properties and fields.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ConditionalDescription that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ConditionalDescription.</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }
}