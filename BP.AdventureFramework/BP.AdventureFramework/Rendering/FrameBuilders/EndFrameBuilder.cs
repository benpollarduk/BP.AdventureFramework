namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for end frames.
    /// </summary>
    public class EndFrameBuilder : IEndFrameBuilder
    {
        #region Implementation of IEndFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(string message, string reason, FrameDrawer frameDrawer, int width, int height)
        {
            var divider = frameDrawer.ConstructDivider(width);
            var constructedScene = divider;

            constructedScene += frameDrawer.ConstructWrappedPaddedString(message, width, true);
            constructedScene += divider;
            constructedScene += frameDrawer.ConstructWrappedPaddedString(reason, width, true);
            constructedScene += divider;
            constructedScene += frameDrawer.ConstructPaddedArea(width, height / 2 - frameDrawer.DetermineLinesInString(constructedScene));
            constructedScene += frameDrawer.ConstructWrappedPaddedString("Press Enter to return to title screen", width, true);
            constructedScene += frameDrawer.ConstructPaddedArea(width, height - frameDrawer.DetermineLinesInString(constructedScene) - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        } 

        #endregion
    }
}
