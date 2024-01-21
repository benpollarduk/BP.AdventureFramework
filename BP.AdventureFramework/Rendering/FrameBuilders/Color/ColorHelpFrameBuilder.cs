﻿using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color help frames.
    /// </summary>
    public sealed class ColorHelpFrameBuilder : IHelpFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the command color.
        /// </summary>
        public AnsiColor CommandColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor CommandDescriptionColor { get; set; } = AnsiColor.Yellow;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorHelpFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public ColorHelpFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
        }

        #endregion

        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, string description, CommandHelp[] commandHelp, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;
            var padding = (commandHelp.Any() ? commandHelp.Max(x => x.Command.Length) : 0) + 1;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawCentralisedWrapped(description, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 2;

            foreach (var command in commandHelp)
            {
                if (lastY >= height - 1)
                    break;

                if (!string.IsNullOrEmpty(command.Command) && !string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.Command, leftMargin, lastY + 1, availableWidth, CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", leftMargin + padding, lastY, availableWidth, CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(command.Description, leftMargin + padding + 2, lastY, availableWidth, CommandDescriptionColor, out _, out lastY);
                }
                else if (!string.IsNullOrEmpty(command.Command) && string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.Command, leftMargin, lastY + 1, availableWidth, CommandColor, out _, out lastY);
                }
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
