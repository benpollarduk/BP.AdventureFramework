using System;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Color
{
    /// <summary>
    /// Provides a builder of simple color conversation frames.
    /// </summary>
    public sealed class SimpleColorConversationFrameBuilder : IConversationFrameBuilder
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
        public AnsiColor BorderColor { get; set; } = AnsiColor.DarkGray;

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
        public AnsiColor ResponseColor { get; set; } = AnsiColor.DarkGray;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = AnsiColor.White;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SimpleColorConversationFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        public SimpleColorConversationFrameBuilder(GridStringBuilder gridStringBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
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
            const int leftMargin = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, leftMargin, lastY, availableWidth, TitleColor, out _, out lastY);
                gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);
                lastY += 2;
            }

            if (converser?.Conversation?.Log != null)
            {
                lastY++;

                var log = converser.Conversation.Log.LastOrDefault();

                if (log != null)
                {
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
                gridStringBuilder.DrawWrapped("You can:", leftMargin, lastY + 3, availableWidth, ResponseColor, out _, out lastY);

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

            gridStringBuilder.DrawWrapped(">", leftMargin, lastY + 2, availableWidth, InputColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, leftMargin + 2, lastY + 2, BackgroundColor) { AcceptsInput = true, ShowCursor = true };
        }

        #endregion
    }
}
