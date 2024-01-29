using System.Linq;

namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a delta.
    /// </summary>
    public sealed class Delta : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the index.
        /// </summary>
        public int Index { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the DeltaInstruction class.
        /// </summary>
        /// <param name="index">The index to shift paragraphs by.</param>
        public Delta(int index)
        {
            Index = index;
        }

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <summary>
        /// Get the index of the next paragraph.
        /// </summary>
        /// <param name="current">The current paragraph.</param>
        /// <param name="collection">The collection of paragraphs.</param>
        /// <returns>The index of the next paragraph.</returns>
        public int GetIndexOfNext(Paragraph current, Paragraph[] collection)
        {
            var currentIndex = collection.ToList().IndexOf(current);
            return currentIndex + Index;
        }

        #endregion
    }
}
