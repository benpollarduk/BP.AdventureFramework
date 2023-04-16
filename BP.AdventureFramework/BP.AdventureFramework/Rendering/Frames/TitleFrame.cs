namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame that can be used as a title screen.
    /// </summary>
    public sealed class TitleFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TitleFrame class
        /// </summary>
        /// <param name="title">The title of the game</param>
        /// <param name="description">A description of the game</param>
        public TitleFrame(string title, string description)
        {
            ShowCursor = false;
            AcceptsInput = false;
            Title = title;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this TitleFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var divider = drawer.ConstructDivider(width);
            var constructedScene = divider;
            constructedScene += drawer.ConstructWrappedPaddedString(Title, width, true);
            constructedScene += divider;
            constructedScene += drawer.ConstructWrappedPaddedString(Description, width, true);
            constructedScene += divider;
            constructedScene += drawer.ConstructPaddedArea(width, height / 2 - drawer.DetermineLinesInString(constructedScene));
            constructedScene += drawer.ConstructWrappedPaddedString("Press Enter to start", width, true);
            constructedScene += drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(constructedScene) - 2);
            constructedScene += divider.Remove(divider.Length - 1);
            return constructedScene;
        }

        #endregion
    }
}