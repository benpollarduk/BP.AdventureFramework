using System.Text;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.LayoutBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy help frames.
    /// </summary>
    public class LegacyHelpFrameBuilder : IHelpFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        public IStringLayoutBuilder StringLayoutBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyHelpFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        public LegacyHelpFrameBuilder(IStringLayoutBuilder stringLayoutBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
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
            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            builder.Append(StringLayoutBuilder.BuildCentralised(title, width));
            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            builder.Append(StringLayoutBuilder.BuildCentralised(description, width));
            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            builder.Append(StringLayoutBuilder.BuildWrappedPadded("COMMANDS", width, false));
            builder.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.Command) && !string.IsNullOrEmpty(command.Command))
                    builder.Append(StringLayoutBuilder.BuildWrappedPadded($"{command.Command}{StringLayoutBuilder.BuildWhitespace(30 - command.Command.Length)}- {command.Description}", width, false));
                else if (!string.IsNullOrEmpty(command.Command) && string.IsNullOrEmpty(command.Description))
                    builder.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
                else
                    builder.Append(StringLayoutBuilder.BuildWrappedPadded(string.Empty, width, false));
            }

            builder.Append(StringLayoutBuilder.BuildPaddedArea(width, height - builder.ToString().LineCount() + 7));
            builder.Append(StringLayoutBuilder.BuildWrappedPadded("Press Enter to return to the game", width, true));
            builder.Append(StringLayoutBuilder.BuildPaddedArea(width, 4));
            var divider = StringLayoutBuilder.BuildHorizontalDivider(width);
            builder.Append(divider.Remove(divider.Length - 1));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
