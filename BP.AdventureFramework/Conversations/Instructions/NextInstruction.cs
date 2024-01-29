using System.Linq;

namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs to the next paragraph.
    /// </summary>
    public sealed class NextInstruction : IEndOfPargraphInstruction
    {
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
            var next = currentIndex + 1;
            var last = collection.Length - 1;
            return next < last ? next : last;
        }

        #endregion
    }
}
