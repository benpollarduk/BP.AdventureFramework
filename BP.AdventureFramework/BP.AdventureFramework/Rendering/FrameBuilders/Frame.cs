namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents a frame for displaying a command based interface.
    /// </summary>
    public sealed class Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the frame data.
        /// </summary>
        private string FrameData { get; }

        /// <summary>
        /// Get the cursor left position.
        /// </summary>
        public int CursorLeft { get; }

        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        public int CursorTop { get; }

        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Get or set if this Frame excepts input.
        /// </summary>
        public bool AcceptsInput { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Frame class.
        /// </summary>
        /// <param name="frameData">The data the frame provides.</param>
        /// <param name="cursorLeft">The cursor left position.</param>
        /// <param name="cursorTop">The cursor top position.</param>
        public Frame(string frameData, int cursorLeft, int cursorTop)
        {
            FrameData = frameData;
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return FrameData;
        }

        #endregion
    }
}