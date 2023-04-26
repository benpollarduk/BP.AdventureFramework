namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    public class TitleFrameBuilder : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string title, string description, FrameDrawer frameDrawer, int width, int height)
        {
            var divider = frameDrawer.ConstructDivider(width);
            var constructedScene = divider;
            constructedScene += frameDrawer.ConstructWrappedPaddedString(title, width, true);
            constructedScene += divider;
            constructedScene += frameDrawer.ConstructWrappedPaddedString(description, width, true);
            constructedScene += divider;
            constructedScene += frameDrawer.ConstructPaddedArea(width, height / 2 - frameDrawer.DetermineLinesInString(constructedScene));
            constructedScene += frameDrawer.ConstructWrappedPaddedString("Press Enter to start", width, true);
            constructedScene += frameDrawer.ConstructPaddedArea(width, height - frameDrawer.DetermineLinesInString(constructedScene) - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
