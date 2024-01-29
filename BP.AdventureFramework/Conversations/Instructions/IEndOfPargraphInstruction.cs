namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// Represents an instructon to be carried out at the end of a paragraph.
    /// </summary>
    public interface IEndOfPargraphInstruction
    {
        /// <summary>
        /// Get the index of the next paragraph.
        /// </summary>
        /// <param name="current">The current paragraph.</param>
        /// <param name="collection">The collection of paragraphs.</param>
        /// <returns>The index of the next paragraph.</returns>
        int GetIndexOfNext(Paragraph current, Paragraph[] collection);
    }
}
