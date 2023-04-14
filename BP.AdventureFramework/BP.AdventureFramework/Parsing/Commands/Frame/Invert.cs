using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Parsing.Commands.Frame
{
    /// <summary>
    /// Represents the Invert command.
    /// </summary>
    public class Invert : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            return new Reaction(ReactionResult.Reacted, "Colours inverted.");
        }

        #endregion
    }
}