namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame that can be used as a title screen
    /// </summary>
    public class TitleFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the title
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Get or set the title
        /// </summary>
        private string title;

        /// <summary>
        /// Get the description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Get or set the description
        /// </summary>
        private string description;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the TitleFrame class
        /// </summary>
        protected TitleFrame()
        {
            // as default don't show
            ShowCursor = false;

            // as default doesn't take input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the TitleFrame class
        /// </summary>
        /// <param name="title">The title of the game</param>
        /// <param name="description">A description of the game</param>
        public TitleFrame(string title, string description)
        {
            // as default don't show
            ShowCursor = false;

            // as default doesn't take input
            AcceptsInput = false;

            // set title
            Title = title;

            // set description
            Description = description;
        }

        /// <summary>
        /// Build this TitleFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // construct a devider
            var devider = drawer.ConstructDevider(width);

            // construct devider
            var constructedScene = devider;

            // add name
            constructedScene += drawer.ConstructWrappedPaddedString(Title, width, true);

            // add another devider
            constructedScene += devider;

            // add name
            constructedScene += drawer.ConstructWrappedPaddedString(Description, width, true);

            // add another devider
            constructedScene += devider;

            // add padded area
            constructedScene += drawer.ConstructPaddedArea(width, height / 2 - drawer.DetermineLinesInString(constructedScene));

            // add command
            constructedScene += drawer.ConstructWrappedPaddedString("Press Enter to start", width, true);

            // add padded area
            constructedScene += drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(constructedScene) - 2);

            // add devider removing the last \n
            constructedScene += devider.Remove(devider.Length - 1);

            // return construction
            return constructedScene;
        }

        #endregion
    }
}