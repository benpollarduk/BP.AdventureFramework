using BP.AdventureFramework.Conversations.Instructions;

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
        /// Get the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.
        /// </summary>
        public IEndOfPargraphInstruction Instruction { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        /// <param name="line">The line to trigger this response.</param>
        public Response(string line) : this(line, new Next())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        /// <param name="line">The line to trigger this response.</param>
        /// <param name="instruction">Specify the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        public Response(string line, IEndOfPargraphInstruction instruction)
        {
            Line = line;
            Instruction = instruction;
        }

        #endregion
    }
}
