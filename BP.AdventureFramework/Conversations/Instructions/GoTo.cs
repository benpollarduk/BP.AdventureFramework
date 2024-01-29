namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on an absolute index.
    /// </summary>
    public sealed class GoTo : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the index.
        /// </summary>
        public int Index { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the GoTo class.
        /// </summary>
        /// <param name="index">The index of the next paragraph.</param>
        public GoTo(int index)
        {
            Index = index;
        }

        #endregion

        #region Implementation of IEndOfPargraphInstruction

        /// <summary>
        /// Get the index of the next paragraph.
        /// </summary>
        /// <param name="current">The current paragraph.</param>
        /// <param name="paragraphs">The collection of paragraphs.</param>
        /// <returns>The index of the next paragraph.</returns>
        public int GetIndexOfNext(Paragraph current, Paragraph[] paragraphs)
        {
            if (Index < 0)
                return 0;

            if (Index >= paragraphs.Length)
                return paragraphs.Length - 1;

            return Index;
        }

        #endregion
    }
}
