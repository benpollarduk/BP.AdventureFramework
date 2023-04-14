using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.Parsing.Commands;

namespace BP.AdventureFramework.Commands.Game
{
    /// <summary>
    /// Represents the Unactionable command.
    /// </summary>
    public class Unactionable : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; } = "Could not react.";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Unactionable class.
        /// </summary>
        public Unactionable()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Unactionable class.
        /// </summary>
        /// <param name="description">The description.</param>
        public Unactionable(string description)
        {
            Description = description;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            return new Reaction(ReactionResult.None, Description);
        }

        #endregion
    }
}