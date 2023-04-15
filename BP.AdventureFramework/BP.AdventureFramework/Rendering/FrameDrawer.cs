using System;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// A class for constructing and drawing Frames.
    /// </summary>
    internal sealed class FrameDrawer : Drawer
    {
        #region Fields

        private readonly char dividerCharacter;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if commands are displayed.
        /// </summary>
        public bool DisplayCommands { get; set; } = true;

        /// <summary>
        /// Occurs when a special frame has been requested to be displayed.
        /// </summary>
        public static event EventHandler<Frame> DisplayedSpecialFrame;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameDrawer class.
        /// </summary>
        /// <param name="leftBoundaryCharacter">The character to use for left boundaries.</param>
        /// <param name="rightBoundaryCharacter">The character to use for right boundaries.</param>
        /// <param name="dividingCharacter">The character to use for dividers.</param>
        public FrameDrawer(char leftBoundaryCharacter = (char)124, char rightBoundaryCharacter = (char)124, char dividingCharacter = (char)45)
        {
            LeftBoundaryCharacter = leftBoundaryCharacter;
            RightBoundaryCharacter = rightBoundaryCharacter;
            dividerCharacter = dividingCharacter;
        }

        /// <summary>
        /// Construct a dividing horizontal line.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <returns>A constructed divider.</returns>
        public string ConstructDivider(int width)
        {
            return ConstructDivider(width, LeftBoundaryCharacter, dividerCharacter, RightBoundaryCharacter);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Request a custom Frame to be displayed to any context listening for the FrameDrawer.DisplaySpecialFrame event.
        /// </summary>
        /// <param name="frame">The frame to display.</param>
        public static void DisplaySpecialFrame(Frame frame)
        {
            DisplayedSpecialFrame?.Invoke(null, frame);
        }

        #endregion
    }
}