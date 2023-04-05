using System;
using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an object that can be examined.
    /// </summary>
    public class ExaminableObject : IExaminable, ITransferableDelegation
    {
        #region StaticProperties

        private static long usedIDS;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Reset the ID seed.
        /// </summary>
        public static void ResetIDSeed()
        {
            usedIDS = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get this objects ID.
        /// </summary>
        internal string ID { get; private set; } = "0";

        /// <summary>
        /// Get or set the callback handling all examination of this object.
        /// </summary>
        public ExaminationCallback Examination { get; set; } = obj => new ExaminationResult(obj.Description != null ? obj.Description.DefaultDescription : obj.GetType().Name);

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ExaminableObject class.
        /// </summary>
        protected ExaminableObject()
        {
            ID = GenerateID();
        }

        /// <summary>
        /// Generate an ID from a name.
        /// </summary>
        /// <returns>The generated ID.</returns>
        protected string GenerateID()
        {
            usedIDS++;
            return Convert.ToString(usedIDS, 16);
        }

        #endregion

        #region IExaminable Members

        /// <summary>
        /// Get the name of this object.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set a description of this object.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get or set if this is player visible.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public virtual ExaminationResult Examime()
        {
            return Examination(this);
        }

        #endregion

        #region ITransferableDelegation Members

        /// <summary>
        /// Generate a transferable ID for this ExaminableObject.
        /// </summary>
        /// <returns>The ID of this object as a string.</returns>
        public virtual string GenerateTransferalID()
        {
            return ID;
        }

        /// <summary>
        /// Transfer delegation to this ExaminableObject from a source ITransferableDelegation object.
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from.</param>
        public virtual void TransferFrom(ITransferableDelegation source)
        {
            Examination = ((ExaminableObject)source).Examination;
        }

        /// <summary>
        /// Register all child properties of this ExaminableObject that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this ExaminableObject.</param>
        public virtual void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            if (!(Description is ITransferableDelegation)) 

                return;

            children.Add(Description as ITransferableDelegation);
            ((ITransferableDelegation)Description).RegisterTransferableChildren(ref children);
        }

        #endregion
    }
}