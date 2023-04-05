using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents any object that can transfer delegation
    /// </summary>
    public interface ITransferableDelegation
    {
        /// <summary>
        /// Generate a transferable ID for this object
        /// </summary>
        /// <returns>The ID as a string</returns>
        string GenerateTransferalID();

        /// <summary>
        /// Transfer delegation to this object from a source ITransferableDelegation object. This sould only concern top level properties and fields
        /// </summary>
        /// <param name="source">The source ITransferableDelegation object to transfer from</param>
        void TransferFrom(ITransferableDelegation source);

        /// <summary>
        /// Register all children that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation of this object</param>
        void RegisterTransferableChildren(ref List<ITransferableDelegation> children);
    }
}