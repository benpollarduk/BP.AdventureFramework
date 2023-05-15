using System.Linq;

namespace BP.AdventureFramework.Conversations
{
    /// <summary>
    /// Represents a paragraph in a Conversation.
    /// </summary>
    public sealed class Paragraph
    {
        #region Properties

        /// <summary>
        /// Get or set the line.
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Get or set the responses, applicable to the last line.
        /// </summary>
        public Response[] Responses { get; set; }

        /// <summary>
        /// Get if a response is possible.
        /// </summary>
        public bool CanRespond => Responses?.Any() ?? false;

        /// <summary>
        /// Get or set any action to carry out on this line.
        /// </summary>
        public ConversationActionCallback Action { get; set; }

        /// <summary>
        /// Get the delta. This can be applied to a conversation to direct the conversation after this paragraph.
        /// </summary>
        public int Delta { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        public Paragraph(string line) : this(line, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="delta">Specify the delta. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        public Paragraph(string line, int delta = 1) : this(line, null, delta)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="action">Specify any action to be carried out with this line.</param>
        /// <param name="delta">Specify the delta. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        public Paragraph(string line, ConversationActionCallback action, int delta = 1)
        {
            Line = line;
            Action = action;
            Delta = delta;
        }

        #endregion
    }
}