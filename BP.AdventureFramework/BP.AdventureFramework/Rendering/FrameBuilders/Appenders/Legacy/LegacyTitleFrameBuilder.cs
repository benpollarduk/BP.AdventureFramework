using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy title frames.
    /// </summary>
    public sealed class LegacyTitleFrameBuilder : ITitleFrameBuilder
    {
        #region Fields

        private readonly LineStringBuilder lineStringBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyTitleFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        public LegacyTitleFrameBuilder(LineStringBuilder lineStringBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
        }

        #endregion

        #region Implementation of ITitleFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, string description, int width, int height)
        {
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(title, width, true);
            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(description, width, true);
            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += lineStringBuilder.BuildWrappedPadded("Press Enter to start", width, true);
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 1);
            constructedScene += divider.Replace(lineStringBuilder.LineTerminator, string.Empty);

            return new TextFrame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
