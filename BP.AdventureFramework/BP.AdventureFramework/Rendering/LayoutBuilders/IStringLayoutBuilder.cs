namespace BP.AdventureFramework.Rendering.LayoutBuilders
{
    /// <summary>
    /// Represents any 
    /// </summary>
    public interface IStringLayoutBuilder
    {
        /// <summary>
        /// Get or set the character used for left boundary.
        /// </summary>
        char LeftBoundaryCharacter { get; set; }
        /// <summary>
        /// Get or set the character used for right boundary.
        /// </summary>
        char RightBoundaryCharacter { get; set; }
        /// <summary>
        /// Get or set the character used for horizontal dividers.
        /// </summary>
        char HorizontalDividerCharacter { get; set; }
        /// <summary>
        /// Build a horizontal divider.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <returns>The divider.</returns>
        string BuildHorizontalDivider(int width);
        /// <summary>
        /// Build a padded area.
        /// </summary>
        /// <param name="width">The width of the padded area.</param>
        /// <param name="height">The height of the padded area.</param>
        /// <returns>A padded area.</returns>
        string BuildPaddedArea(int width, int height);
        /// <summary>
        /// Build a wrapped padded string.
        /// </summary>
        /// <param name="value">The string to pad.</param>
        /// <param name="width">The overall width of the padded string.</param>
        /// <param name="centralise">True if the string should be centralised.</param>
        /// <returns>The padded string.</returns>
        string BuildWrappedPadded(string value, int width, bool centralise);
        /// <summary>
        /// Build a string made of whitespace.
        /// </summary>
        /// <param name="width">The width of the whitespace.</param>
        /// <returns>The whitespace string.</returns>
        string BuildWhitespace(int width);
        /// <summary>
        /// Build a centralised string.
        /// </summary>
        /// <param name="value">The string to centralise.</param>
        /// <param name="width">The overall width of the string.</param>
        /// <returns>The centralised string.</returns>
        string BuildCentralised(string value, int width);
    }
}
