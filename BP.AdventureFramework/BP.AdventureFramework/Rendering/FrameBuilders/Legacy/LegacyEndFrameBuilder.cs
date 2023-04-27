using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.LayoutBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder for legacy end frames.
    /// </summary>
    public class LegacyEndFrameBuilder : IEndFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        public IStringLayoutBuilder StringLayoutBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyEndFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        public LegacyEndFrameBuilder(IStringLayoutBuilder stringLayoutBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
        }

        #endregion

        #region Implementation of IEndFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string message, string reason, int width, int height)
        {
            var divider = StringLayoutBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;

            constructedScene += StringLayoutBuilder.BuildWrappedPadded(message, width, true);
            constructedScene += divider;
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(reason, width, true);
            constructedScene += divider;
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += StringLayoutBuilder.BuildWrappedPadded("Press Enter to return to title screen", width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        } 

        #endregion
    }
}
