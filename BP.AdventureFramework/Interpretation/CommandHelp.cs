using System;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    public sealed class CommandHelp : IEquatable<CommandHelp>
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

        #region Implementation of IEquatable<ExaminableIdentifier>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(CommandHelp other)
        {
            return Command == other?.Command && Description == other?.Description;
        }

        #endregion
    }
}
