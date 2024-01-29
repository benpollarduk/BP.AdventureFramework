using System.Linq;

namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a delta.
    /// </summary>
    public sealed class Jump : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the delta.
        /// </summary>
        public int Delta { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the Jump class.
        /// </summary>
        /// <param name="delta">The delta to shift paragraphs by.</param>
        public Jump(int delta)
        {
            Delta = delta;
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
            var offset = currentIndex + Delta;

            if (offset < 0)
                return 0;

            if (offset >= collection.Length)
                return collection.Length - 1;

            return offset;
        }

        #endregion
    }
}
