using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder for legacy game over frames.
    /// </summary>
    public sealed class LegacyGameOverFrameBuilder : IGameOverFrameBuilder
    {
        #region Fields

        private readonly LineStringBuilder lineStringBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyGameOverFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        public LegacyGameOverFrameBuilder(LineStringBuilder lineStringBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
        }

        #endregion

        #region Implementation of IGameOverFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string message, string reason, int width, int height)
        {
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;

            constructedScene += lineStringBuilder.BuildWrappedPadded(message, width, true);
            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(reason, width, true);
            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += lineStringBuilder.BuildWrappedPadded("Press Enter to return to the title screen", width, true);
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 1);
            constructedScene += divider.Replace(lineStringBuilder.LineTerminator, string.Empty);

            return new TextFrame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        } 

        #endregion
    }
}
