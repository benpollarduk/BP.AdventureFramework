using BP.AdventureFramework.Conversations;

namespace BP.AdventureFramework.Assets.Characters
{
    /// <summary>
    /// Represents an object that can converse.
    /// </summary>
    public interface IConverser : IExaminable
    {
        /// <summary>
        /// Get or set the conversation.
        /// </summary>
        Conversation Conversation { get; set; }
    }
}