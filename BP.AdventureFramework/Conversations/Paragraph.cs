using System.Linq;
using BP.AdventureFramework.Conversations.Instructions;

namespace BP.AdventureFramework.Conversations
{
    /// <summary>
    /// Represents a paragraph in a Conversation.
    /// </summary>
    public sealed class Paragraph
    {
        #region Properties

        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; private set; }

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
        /// Get the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.
        /// </summary>
        public IEndOfPargraphInstruction Instruction { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="name">Specify the name of the paragraph.</param>
        public Paragraph(string line, string name = "") : this(line, null, new DeltaInstruction(1), name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="delta">Specify delta to shift paragraphs by at the end of this paragraph. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        /// <param name="name">Specify the name of the paragraph.</param>
        public Paragraph(string line, int delta, string name = "") : this(line, null, new DeltaInstruction(delta), name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="action">Specify any action to be carried out with this line.</param>
        /// <param name="delta">Specify delta to shift paragraphs by at the end of this paragraph. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        /// <param name="name">Specify the name of the paragraph.</param>
        public Paragraph(string line, ConversationActionCallback action, int delta, string name = "") : this(line, action, new DeltaInstruction(delta), name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Paragraph class.
        /// </summary>
        /// <param name="line">Specify the line.</param>
        /// <param name="action">Specify any action to be carried out with this line.</param>
        /// <param name="instruction">Specify the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.</param>
        /// <param name="name">Specify the name of the paragraph.</param>
        public Paragraph(string line, ConversationActionCallback action, IEndOfPargraphInstruction instruction, string name = "")
        {
            Line = line;
            Action = action;
            Instruction = instruction;
            Name = name;
        }

        #endregion
    }
}