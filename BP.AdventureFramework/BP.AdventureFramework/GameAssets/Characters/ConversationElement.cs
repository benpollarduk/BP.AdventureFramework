using System;

namespace BP.AdventureFramework.GameAssets.Characters
{
    /// <summary>
    /// Represents an element of a Conversation.
    /// </summary>
    public class ConversationElement
    {
        #region Properties

        /// <summary>
        /// Get or set the line.
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Get or set any action to carry out on this line.
        /// </summary>
        public Action Action { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConversationElement class.
        /// </summary>
        /// <param name="line">Specify the line in this conversation.</param>
        public ConversationElement(string line)
        {
            Line = line;
        }

        /// <summary>
        /// Initializes a new instance of the ConversationElement class.
        /// </summary>
        /// <param name="line">Specify the line in this conversation.</param>
        /// <param name="action">Specify any action to be carried out with this line.</param>
        public ConversationElement(string line, Action action)
        {
            Line = line;
            Action = action;
        }

        #endregion
    }
}