using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy title frames.
    /// </summary>
    public class LegacyTitleFrameBuilder : ITitleFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the drawer.
        /// </summary>
        public Drawer Drawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyTitleFrameBuilder class.
        /// </summary>
        /// <param name="drawer">A drawer to use for the frame.</param>
        public LegacyTitleFrameBuilder(Drawer drawer)
        {
            Drawer = drawer;
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
            var divider = Drawer.ConstructDivider(width);
            var constructedScene = divider;
            constructedScene += Drawer.ConstructWrappedPaddedString(title, width, true);
            constructedScene += divider;
            constructedScene += Drawer.ConstructWrappedPaddedString(description, width, true);
            constructedScene += divider;
            constructedScene += Drawer.ConstructPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += Drawer.ConstructWrappedPaddedString("Press Enter to start", width, true);
            constructedScene += Drawer.ConstructPaddedArea(width, height - constructedScene.LineCount() - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
