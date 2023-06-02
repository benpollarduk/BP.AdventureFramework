using System;
using System.Drawing;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color
{
    /// <summary>
    /// Provides a builder of color title frames.
    /// </summary>
    public sealed class ColorTitleFrameBuilder : ITitleFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public RenderColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public RenderColor BorderColor { get; set; } = RenderColor.DarkGray;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public RenderColor TitleColor { get; set; } = RenderColor.White;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public RenderColor DescriptionColor { get; set; } = RenderColor.White;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorTitleFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public ColorTitleFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
        }

        #endregion

        #region Implementation of ITitleFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, string description, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            gridStringBuilder.DrawWrapped(description.EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, DescriptionColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
