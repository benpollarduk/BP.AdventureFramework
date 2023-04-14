using System.Collections.Generic;
using System.Linq;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents an in-game conversation with a character.
    /// </summary>
    public class Conversation
    {
        #region Properties

        /// <summary>
        /// Get the lines of this conversation.
        /// </summary>
        public List<ConversationElement> Lines { get; protected set; } = new List<ConversationElement>();

        /// <summary>
        /// Get the current line of this conversation.
        /// </summary>
        public int CurrentLine { get; protected set; }

        /// <summary>
        /// Get if this has some remaining lines.
        /// </summary>
        public bool HasSomeRemainingLines
        {
            get { return CurrentLine < Lines.Count; }
        }

        /// <summary>
        /// Get or set if the last element of the conversation should be repeated.
        /// </summary>
        public bool RepeatLastElement { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        /// <param name="lines">The lines to add in this conversation.</param>
        public Conversation(params ConversationElement[] lines)
        {
            Lines.AddRange(lines);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the next line of the conversation.
        /// </summary>
        /// <returns>The next line of the conversation.</returns>
        public string NextLine()
        {
            if (HasSomeRemainingLines)
            {
                var e = Lines[CurrentLine];
                e.Action?.Invoke();
                CurrentLine++;
                return e.Line;
            }

            if (!RepeatLastElement || !Lines.Any())
                return string.Empty;

            var element = Lines[Lines.Count - 1];
            element.Action?.Invoke();
            return element.Line;
        }

        #endregion
    }
}