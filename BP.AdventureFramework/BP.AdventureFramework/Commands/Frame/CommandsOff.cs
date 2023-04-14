using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Parsing.Commands;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOff command.
    /// </summary>
    public class CommandsOff : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandsOff class.
        /// </summary>
        /// <param name="frameDrawer">The frame drawer.</param>
        public CommandsOff(FrameDrawer frameDrawer)
        {
            FrameDrawer = frameDrawer;
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <returns>The reaction.</returns>
        public Reaction Invoke()
        {
            if (FrameDrawer == null)
                return new Reaction(ReactionResult.None, "No frame drawer specified.");

            FrameDrawer.DisplayCommands = false;
            return new Reaction(ReactionResult.Reacted, "Commands have been turned off.");
        }

        #endregion
    }
}