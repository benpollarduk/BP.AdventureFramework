namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    public class CommandHelp
    {
        #region Properties

        /// <summary>
        /// Get the command.
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Get the description of the command.
        /// </summary>
        public string Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandHelp class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">The help.</param>
        public CommandHelp(string command, string description)
        {
            Command = command;
            Description = description;
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CommandHelp other)
                return Command == other.Command && Description == other.Description;

            return false;
        }

        #endregion
    }
}
