using BP.AdventureFramework.GameAssets.Interaction;
using BP.AdventureFramework.Parsing.Commands;
using BP.AdventureFramework.Rendering;

namespace BP.AdventureFramework.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOn command.
    /// </summary>
    public class CommandsOn : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandsOn class.
        /// </summary>
        /// <param name="frameDrawer">The frame drawer.</param>
        public CommandsOn(FrameDrawer frameDrawer)
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

            FrameDrawer.DisplayCommands = true;
            return new Reaction(ReactionResult.Reacted, "Commands have been turned on.");
        }

        #endregion
    }
}