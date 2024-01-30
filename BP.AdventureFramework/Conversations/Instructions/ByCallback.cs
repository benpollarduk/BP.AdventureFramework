using System;

namespace BP.AdventureFramework.Conversations.Instructions
{
    /// <summary>
    /// An end of paragraph instruction that shifts paragraphs based on a callback.
    /// </summary>
    public sealed class ByCallback : IEndOfPargraphInstruction
    {
        #region Properties

        /// <summary>
        /// Get the callback that decides the paragraph index.
        /// </summary>
        public Func<int> Callback { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the ByCallback class.
        /// </summary>
        /// <param name="callback">The callback that decides the paragraph index.</param>
        public ByCallback(Func<int> callback)
        {
            Callback = callback;
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
            return Callback.Invoke();
        }

        #endregion
    }
}
