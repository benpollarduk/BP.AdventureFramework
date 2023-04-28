using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.LayoutBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy title frames.
    /// </summary>
    public sealed class LegacyTitleFrameBuilder : ITitleFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        private IStringLayoutBuilder StringLayoutBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyTitleFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        public LegacyTitleFrameBuilder(IStringLayoutBuilder stringLayoutBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
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
        public Frame Build(string title, string description, int width, int height)
        {
            var divider = StringLayoutBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(title, width, true);
            constructedScene += divider;
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(description, width, true);
            constructedScene += divider;
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += StringLayoutBuilder.BuildWrappedPadded("Press Enter to start", width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 1);
            constructedScene += divider.Replace(StringLayoutBuilder.LineTerminator, string.Empty);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
