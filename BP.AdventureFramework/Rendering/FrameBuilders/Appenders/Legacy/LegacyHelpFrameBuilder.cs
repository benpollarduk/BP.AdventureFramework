using System.Text;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy help frames.
    /// </summary>
    public sealed class LegacyHelpFrameBuilder : IHelpFrameBuilder
    {
        #region Fields

        private readonly LineStringBuilder lineStringBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyHelpFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        public LegacyHelpFrameBuilder(LineStringBuilder lineStringBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
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
        public IFrame Build(string title, string description, CommandHelp[] commandHelp, int width, int height)
        {
            var builder = new StringBuilder();
            builder.Append(lineStringBuilder.BuildHorizontalDivider(width));
            builder.Append(lineStringBuilder.BuildCentralised(title, width));
            builder.Append(lineStringBuilder.BuildHorizontalDivider(width));
            builder.Append(lineStringBuilder.BuildCentralised(description, width));
            builder.Append(lineStringBuilder.BuildHorizontalDivider(width));
            builder.Append(lineStringBuilder.BuildWrappedPadded(string.Empty, width, false));

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.Command) && !string.IsNullOrEmpty(command.Description))
                    builder.Append(lineStringBuilder.BuildWrappedPadded($"{command.Command}{lineStringBuilder.BuildWhitespace(30 - command.Command.Length)}- {command.Description}", width, false));
                else if (!string.IsNullOrEmpty(command.Command) && string.IsNullOrEmpty(command.Description))
                    builder.Append(lineStringBuilder.BuildWrappedPadded(string.Empty, width, false));
                else
                    builder.Append(lineStringBuilder.BuildWrappedPadded(string.Empty, width, false));
            }

            builder.Append(lineStringBuilder.BuildPaddedArea(width, height - builder.ToString().LineCount() - 1));
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            builder.Append(divider.Replace(lineStringBuilder.LineTerminator, string.Empty));

            return new TextFrame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
