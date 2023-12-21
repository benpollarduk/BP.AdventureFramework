namespace BP.AdventureFramework.Conversations
{
    /// <summary>
    /// Provides a container for log items.
    /// </summary>
    public sealed class LogItem
    {
        #region Properties

        /// <summary>
        /// Get the participant.
        /// </summary>
        public Participant Participant { get; }

        /// <summary>
        /// Get the line.
        /// </summary>
        public string Line { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogItem class.
        /// </summary>
        /// <param name="participant">The participant.</param>
        /// <param name="line">The line.</param>
        public LogItem(Participant participant, string line)
        {
            Participant = participant;
            Line = line;
        }

        #endregion
    }
}