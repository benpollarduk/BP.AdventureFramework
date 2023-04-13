using System.Collections.Generic;
using System.Linq;

namespace BP.AdventureFramework.Characters
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
        /// Reset this conversation.
        /// </summary>
        public void Reset()
        {
            CurrentLine = 0;
        }

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

        /// <summary>
        /// Add a line to this conversation.
        /// </summary>
        /// <param name="line">The line to add to this conversation.</param>
        public void AddLine(string line)
        {
            Lines.Add(new ConversationElement(line));
        }

        /// <summary>
        /// Add a line to this conversation.
        /// </summary>
        /// <param name="line">The line to add to this conversation.</param>
        public void AddLine(ConversationElement line)
        {
            Lines.Add(line);
        }

        /// <summary>
        /// Truncate this conversation at the current line.
        /// </summary>
        public void Truncate()
        {
            for (var index = 0; index < CurrentLine; index++)
                Lines.RemoveAt(index);
        }

        #endregion
    }
}