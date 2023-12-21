namespace BP.AdventureFramework.Assets
{
    /// <summary>
    /// Represents a size.
    /// </summary>
    public struct Size
    {
        #region Properties

        /// <summary>
        /// Get the width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Get the height.
        /// </summary>
        public int Height { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Size struct.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #endregion
    }
}
