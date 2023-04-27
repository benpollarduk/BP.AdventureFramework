using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder for legacy end frames.
    /// </summary>
    public class LegacyEndFrameBuilder : IEndFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyEndFrameBuilder class.
        /// </summary>
        /// <param name="frameDrawer">A drawer to use for the frame.</param>
        public LegacyEndFrameBuilder(FrameDrawer frameDrawer)
        {
            FrameDrawer = frameDrawer;
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
            var divider = FrameDrawer.ConstructDivider(width);
            var constructedScene = divider;

            constructedScene += FrameDrawer.ConstructWrappedPaddedString(message, width, true);
            constructedScene += divider;
            constructedScene += FrameDrawer.ConstructWrappedPaddedString(reason, width, true);
            constructedScene += divider;
            constructedScene += FrameDrawer.ConstructPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += FrameDrawer.ConstructWrappedPaddedString("Press Enter to return to title screen", width, true);
            constructedScene += FrameDrawer.ConstructPaddedArea(width, height - constructedScene.LineCount() - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        } 

        #endregion
    }
}
