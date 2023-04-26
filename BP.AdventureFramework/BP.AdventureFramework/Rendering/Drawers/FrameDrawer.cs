namespace BP.AdventureFramework.Rendering.Drawers
{
    /// <summary>
    /// A class for constructing and drawing Frames.
    /// </summary>
    public sealed class FrameDrawer : Drawer
    {
        #region Fields

        private readonly char dividerCharacter;

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

        #endregion

        #region Methods

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
    }
}