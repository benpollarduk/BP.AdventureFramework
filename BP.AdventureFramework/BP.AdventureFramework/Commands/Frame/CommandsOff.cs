using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOff command.
    /// </summary>
    internal class CommandsOff : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            SceneFrame.DisplayCommands = false;
            return new Reaction(ReactionResult.Reacted, "Commands have been turned off.");
        }

        #endregion
    }
}