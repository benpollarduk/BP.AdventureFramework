namespace BP.AdventureFramework.Conversations
{
    /// <summary>
    /// Provides a response to a conversation.
    /// </summary>
    public sealed class Response
    {
        #region Properties

        /// <summary>
        /// Get the line.
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// Get the delta. This can be applied to a conversation to direct the conversation after this paragraph.
        /// </summary>
        public int Delta { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        /// <param name="line">The line to trigger this response.</param>
        /// <param name="delta">Specify the delta. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        public Response(string line, int delta = 1)
        {
            Line = line;
            Delta = delta;
        }

        #endregion
    }
}
