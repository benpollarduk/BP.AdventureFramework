using System;
using System.Collections.Generic;
using System.Xml;
using AdventureFramework.IO;

namespace BP.AdventureFramework.Interaction
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
        private static long usedIDS;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Reset the ID seed
        /// </summary>
        public static void ResetIDSeed()
        {
            // reset seed
            usedIDS = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the name of this ExaminableObject
        /// </summary>
        protected string name = string.Empty;

        /// <summary>
        /// Get or set the description of this ExaminableObject
        /// </summary>
        protected Description description;

        /// <summary>
        /// Get or set if this is visible to the player
        /// </summary>
        protected bool isPlayerVisible = true;

        /// <summary>
        /// Get this objects ID
        /// </summary>
        internal string ID
        {
            get { return id ?? "0"; }
            private set { id = value; }
        }

        /// <summary>
        /// Get or set this objects ID
        /// </summary>
        private string id;

        /// <summary>
        /// Get or set the callback handling all examination of this object
        /// </summary>
        public ExaminationCallback Examination
        {
            get { return examination; }
            set { examination = value; }
        }

        /// <summary>
        /// Get or set the callback handling all examination of this object
        /// </summary>
        private ExaminationCallback examination = obj =>
        {
            // compile default examination
            return new ExaminationResult(obj.Description != null ? obj.Description.GetDescription() : obj.GetType().Name);
        };

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminableObject class
        /// </summary>
        protected ExaminableObject()
        {
            // generate ID
            ID = GenerateID();
        }

        /// <summary>
        /// Generate an ID from a name
        /// </summary>
        /// <returns>The generated ID</returns>
        protected virtual string GenerateID()
        {
            // increment used ID's
            usedIDS++;

            // return ID as hex
            return Convert.ToString(usedIDS, 16);
        }

        /// <summary>
        /// Handle examination this object
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected virtual ExaminationResult OnExamined()
        {
            return Examination(this);
        }

        /// <summary>
        /// Handle generation of a transferable ID for this ExaminableObject
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        protected virtual string OnGenerateTransferalID()
        {
            return ID;
        }

        /// <summary>
        /// Handle transferal of delegation to this ExaminableObject from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        protected virtual void OnTransferFrom(ITransferableDelegation source)
        {
            // set examination
            Examination = ((ExaminableObject)source).Examination;
        }

        /// <summary>
        /// Handle registration of all child properties of this ExaminableObject that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject</param>
        protected virtual void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // sometimes a description may be transferable
            if (Description is ITransferableDelegation)
            {
                // add description
                children.Add(Description as ITransferableDelegation);

                // register children
                ((ITransferableDelegation)Description).RegisterTransferableChildren(ref children);
            }
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this ExaminableObject
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("ExaminableObject");

            // write name
            writer.WriteAttributeString("Name", Name);

            // write if player visible
            writer.WriteAttributeString("IsPlayerVisible", IsPlayerVisible.ToString());

            // write id
            writer.WriteAttributeString("ID", ID);

            // write description
            Description.WriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this ExaminableObject
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // get name
            name = GetAttribute(node, "Name").Value;

            // get if player visible
            isPlayerVisible = bool.Parse(GetAttribute(node, "IsPlayerVisible").Value);

            // parse ID
            ID = GetAttribute(node, "ID").Value;

            // if a conditional descripition
            if (NodeExists(node, "ConditionalDescription"))
            {
                // if no description
                if (Description == null)
                    // create new description
                    Description = new ConditionalDescription(string.Empty, string.Empty, null);

                // read description
                Description.ReadXmlNode(GetNode(node, "ConditionalDescription"));
            }
            else
            {
                // if no description
                if (Description == null)
                    // create new description
                    Description = new Description(string.Empty);

                // read description
                Description.ReadXmlNode(GetNode(node, "Description"));
            }
        }

        #endregion

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get the name of this object
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Get or set a description of this object
        /// </summary>
        public Description Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Get or set if this is player visible
        /// </summary>
        public bool IsPlayerVisible
        {
            get { return isPlayerVisible; }
            set { isPlayerVisible = value; }
        }

        /// <summary>
        /// Examine this object
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object</returns>
        public ExaminationResult Examime()
        {
            return OnExamined();
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ExaminableObject
        /// </summary>
        /// <returns>The ID of this object as a string</returns>
        public string GenerateTransferalID()
        {
            return OnGenerateTransferalID();
        }

        /// <summary>
        /// Transfer delegation to this ExaminableObject from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        public void TransferFrom(ITransferableDelegation source)
        {
            OnTransferFrom(source);
        }

        /// <summary>
        /// Register all child properties of this ExaminableObject that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject</param>
        public void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            OnRegisterTransferableChildren(ref children);
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