using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an object that can be examined
    /// </summary>
    public class ExaminableObject : XMLSerializableObject, IExaminable, ITransferableDelegation
    {
        #region StaticProperties

        /// <summary>
        /// Get or set the used ID's to date
        /// </summary>
        private static Int64 usedIDS = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the name of this ExaminableObject
        /// </summary>
        protected String name = String.Empty;

        /// <summary>
        /// Get or set the description of this ExaminableObject
        /// </summary>
        protected Description description;

        /// <summary>
        /// Get or set if this is visible to the player
        /// </summary>
        protected Boolean isPlayerVisible = true;

        /// <summary>
        /// Get this objects ID
        /// </summary>
        internal String ID
        {
            get { return this.id ?? "0"; }
            private  set { this.id = value; }
        }

        /// <summary>
        /// Get or set this objects ID
        /// </summary>
        private String id = null;

        /// <summary>
        /// Get or set the callback handling all examination of this object
        /// </summary>
        public ExaminationCallback Examination
        {
            get { return this.examination; }
            set { this.examination = value; }
        }

        /// <summary>
        /// Get or set the callback handling all examination of this object
        /// </summary>
        private ExaminationCallback examination = new ExaminationCallback((IExaminable obj) =>
        {
            // compile default examination
            return new ExaminationResult(obj.Description != null ? obj.Description.GetDescription() : obj.GetType().Name);
        });

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminableObject class
        /// </summary>
        protected ExaminableObject()
        {
            // generate ID
            this.ID = this.GenerateID();
        }

        /// <summary>
        /// Generate an ID from a name
        /// </summary>
        /// <returns>The generated ID</returns>
        protected virtual String GenerateID()
        {
            // increment used ID's
            ExaminableObject.usedIDS++;

            // return ID as hex
            return Convert.ToString(ExaminableObject.usedIDS, 16);
        }

        /// <summary>
        /// Handle examination this object
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected virtual ExaminationResult OnExamined()
        {
            return this.Examination(this);
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ExaminableObject
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        protected virtual String OnGenerateTransferalID()
        {
            return this.ID;
        }

        /// <summary>
        /// Handle transferal of delegation to this ExaminableObject from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            // set examination
            this.Examination = ((ExaminableObject)source).Examination;
        }

        /// <summary>
        /// Handle registration of all child properties of this ExaminableObject that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // sometimes a description may be transferable
            if (this.Description is ITransferableDelegation)
            {
                // add description
                children.Add(this.Description as ITransferableDelegation);

                // register children
                ((ITransferableDelegation)this.Description).RegisterTransferableChildren(ref children);
            }
            else
            {
                // no children to register
            }
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this ExaminableObject
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("ExaminableObject");
            
            // write name
            writer.WriteAttributeString("Name", this.Name.ToString());
            
            // write if player visible
            writer.WriteAttributeString("IsPlayerVisible", this.IsPlayerVisible.ToString());

            // write id
            writer.WriteAttributeString("ID", this.ID.ToString());

            // write description
            this.Description.WriteXml(writer);
            
            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ExaminableObject
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get name
            this.name = XMLSerializableObject.GetAttribute(node, "Name").Value;

            // get if player visible
            this.isPlayerVisible = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "IsPlayerVisible").Value);

            // parse ID
            this.ID = XMLSerializableObject.GetAttribute(node, "ID").Value;

            // if a conditional descripition
            if (XMLSerializableObject.NodeExists(node, "ConditionalDescription"))
            {
                // if no description
                if (this.Description == null)
                {
                    // create new description
                    this.Description = new ConditionalDescription(String.Empty, String.Empty, null);
                }

                // read description
                this.Description.ReadXmlNode(XMLSerializableObject.GetNode(node, "ConditionalDescription"));
            }
            else
            {
                // if no description
                if (this.Description == null)
                {
                    // create new description
                    this.Description = new Description(String.Empty);
                }

                // read description
                this.Description.ReadXmlNode(XMLSerializableObject.GetNode(node, "Description"));
            }
        }

        #endregion

        #endregion

        #region StaticMethods

        /// <summary>
        /// Reset the ID seed
        /// </summary>
        public static void ResetIDSeed()
        {
            // reset seed
            ExaminableObject.usedIDS = 0;
        }

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get the name of this object
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Get or set a description of this object
        /// </summary>
        public Description Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Get or set if this is player visible
        /// </summary>
        public bool IsPlayerVisible
        {
            get { return this.isPlayerVisible; } 
            set { this.isPlayerVisible = value; }
        }

        /// <summary>
        /// Examine this object
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object</returns>
        public ExaminationResult Examime()
        {
            return this.OnExamined();
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ExaminableObject
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        public string GenerateTransferalID()
        {
            return this.OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ExaminableObject from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            this.OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ExaminableObject that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            this.OnRegisterTransferableChildren(ref children);
        }

        #endregion
    }

    /// <summary>
    /// Represents the callback for examinations
    /// </summary>
    /// <param name="obj">The object to examine</param>
    /// <returns>A string representing the result of the examination</returns>
    public delegate ExaminationResult ExaminationCallback(IExaminable obj);
}
