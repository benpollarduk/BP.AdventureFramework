using System.Text;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy help frames.
    /// </summary>
    public class LegacyHelpFrameBuilder : IHelpFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the drawer.
        /// </summary>
        public Drawer Drawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyHelpFrameBuilder class.
        /// </summary>
        /// <param name="drawer">A drawer to use for the frame.</param>
        public LegacyHelpFrameBuilder(Drawer drawer)
        {
            Drawer = drawer;
        }

        #endregion

        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string title, string description, CommandHelp[] commandHelp, int width, int height)
        {
            var builder = new StringBuilder();
            builder.Append(Drawer.ConstructDivider(width));
            builder.Append(Drawer.ConstructCentralisedString(title, width));
            builder.Append(Drawer.ConstructDivider(width));
            builder.Append(Drawer.ConstructCentralisedString(description, width));
            builder.Append(Drawer.ConstructDivider(width));
            builder.Append(Drawer.ConstructWrappedPaddedString("COMMANDS", width, false));
            builder.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width, false));

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.Command) && !string.IsNullOrEmpty(command.Command))
                    builder.Append(Drawer.ConstructWrappedPaddedString($"{command.Command}{Drawer.ConstructWhitespaceString(30 - command.Command.Length)}- {command.Description}", width, false));
                else if (!string.IsNullOrEmpty(command.Command) && string.IsNullOrEmpty(command.Description))
                    builder.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
                else
                    builder.Append(Drawer.ConstructWrappedPaddedString(string.Empty, width));
            }

            builder.Append(Drawer.ConstructPaddedArea(width, height - builder.ToString().LineCount() + 7));
            builder.Append(Drawer.ConstructWrappedPaddedString("Press Enter to return to the game", width, true));
            builder.Append(Drawer.ConstructPaddedArea(width, 4));
            var divider = Drawer.ConstructDivider(width);
            builder.Append(divider.Remove(divider.Length - 1));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
