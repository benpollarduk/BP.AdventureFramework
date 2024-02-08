using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of color conversation frames.
    /// </summary>
    public sealed class ColorConversationFrameBuilder : IConversationFrameBuilder
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
        public AnsiColor TitleColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the player message color.
        /// </summary>
        public AnsiColor NonPlayerMessageColor { get; set; } = AnsiColor.Yellow;

        /// <summary>
        /// Get or set the player message color.
        /// </summary>
        public AnsiColor PlayerMessageColor { get; set; } = AnsiColor.Blue;

        /// <summary>
        /// Get or set the response color.
        /// </summary>
        public AnsiColor ResponseColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = AnsiColor.White;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorConversationFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public ColorConversationFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Truncate a log.
        /// </summary>
        /// <param name="startX">The start X position, in the grid.</param>
        /// <param name="availableWidth">The available width.</param>
        /// <param name="availableHeight">The available height.</param>
        /// <param name="log">The log.</param>
        /// <returns>The truncated log.</returns>
        internal LogItem[] TruncateLog(int startX, int availableWidth, int availableHeight, LogItem[] log)
        {
            if (log == null)
                return Array.Empty<LogItem>();

            var truncated = new List<LogItem>();

            for (var i = log.Length - 1; i >= 0; i--)
            {
                var lines = gridStringBuilder.GetNumberOfLines(log[i].Line, startX, 0, availableWidth);

                availableHeight -= lines;

                if (availableHeight >= 0)
                    truncated.Add(log[i]);
                else
                    break;
            }

            truncated.Reverse();

            return truncated.ToArray();
        }

        #endregion

        #region Implementation of IConversationFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="converser">The converser.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            var availableHeight = height - 2;
            const int leftMargin = 2;
            const int linePadding = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, leftMargin, lastY, availableWidth, TitleColor, out _, out lastY);
                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);
                lastY += 2;
            }

            if (converser?.Conversation?.Log != null)
            {
                var spaceForLog = availableHeight - 10 - contextualCommands?.Length ?? 0;
                var truncatedLog = TruncateLog(leftMargin, availableWidth, spaceForLog, converser.Conversation.Log);

                foreach (var log in truncatedLog)
                {
                    lastY++;
                    switch (log.Participant)
                    {
                        case Participant.Player:
                            gridStringBuilder.DrawWrapped("You: " + log.Line, leftMargin, lastY, availableWidth, PlayerMessageColor, out _, out lastY);
                            break;
                        case Participant.Other:
                            gridStringBuilder.DrawWrapped($"{converser.Identifier.Name}: " + log.Line, leftMargin, lastY, availableWidth, NonPlayerMessageColor, out _, out lastY);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            if (contextualCommands?.Any() ?? false)
            {
                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, BorderColor);
                gridStringBuilder.DrawWrapped("You can:", leftMargin, lastY + 4, availableWidth, ResponseColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.Command.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                foreach (var contextualCommand in contextualCommands)
                {
                    gridStringBuilder.DrawWrapped(contextualCommand.Command, leftMargin, lastY + 1, availableWidth, ResponseColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, ResponseColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description, descriptionStartX, lastY, availableWidth, ResponseColor, out _, out lastY);
                }
            }

            gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
            gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, leftMargin + 2, availableHeight, BackgroundColor) { AcceptsInput = true, ShowCursor = true };
        }

        #endregion
    }
}
