namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for when the game ends.
    /// </summary>
    public class EndFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the message.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Get the reason.
        /// </summary>
        public string Reason { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EndFrame class.
        /// </summary>
        protected EndFrame()
        {
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the EndFrame class.
        /// </summary>
        /// <param name="message">A message to show the user.</param>
        /// <param name="reason">The reason for the end.</param>
        public EndFrame(string message, string reason)
        {
            ShowCursor = false;
            AcceptsInput = false;
            Message = message;
            Reason = reason;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this EndFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var divider = drawer.ConstructDivider(width);
            var constructedScene = divider;

            constructedScene += drawer.ConstructWrappedPaddedString(Message, width, true);
            constructedScene += divider;
            constructedScene += drawer.ConstructWrappedPaddedString(Reason, width, true);
            constructedScene += divider;
            constructedScene += drawer.ConstructPaddedArea(width, height / 2 - drawer.DetermineLinesInString(constructedScene));
            constructedScene += drawer.ConstructWrappedPaddedString("Press Enter to return to title screen", width, true);
            constructedScene += drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(constructedScene) - 2);
            constructedScene += divider.Remove(divider.Length - 1);

            return constructedScene;
        }

        #endregion
    }
}