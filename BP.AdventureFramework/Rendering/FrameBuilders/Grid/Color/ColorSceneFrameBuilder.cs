﻿using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color
{
    /// <summary>
    /// Provides a builder for color scene frames.
    /// </summary>
    public sealed class ColorSceneFrameBuilder : ISceneFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder;
        private readonly IRoomMapBuilder roomMapBuilder;

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
        /// Get or set the text color.
        /// </summary>
        public RenderColor TextColor { get; set; } = RenderColor.White;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public RenderColor InputColor { get; set; } = RenderColor.White;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public RenderColor CommandsColor { get; set; } = RenderColor.DarkGray;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorSceneFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        /// <param name="roomMapBuilder">A builder to use for room maps.</param>
        public ColorSceneFrameBuilder(GridStringBuilder gridStringBuilder, IRoomMapBuilder roomMapBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
            this.roomMapBuilder = roomMapBuilder;
        }

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="message">Any additional message.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, string message, CommandHelp[] contextualCommands, KeyType keyType, int width, int height)
        {
            var availableWidth = width - 4;
            var availableHeight = height - 2;
            const int leftMargin = 2;
            const int linePadding = 2;

            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            gridStringBuilder.DrawWrapped(room.Identifier.Name, leftMargin, 2, availableWidth, TextColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, room.Identifier.Name.Length, TextColor);

            gridStringBuilder.DrawWrapped(room.Description.GetDescription().EnsureFinishedSentence(), 2, lastY + 3, availableWidth, TextColor, out _, out lastY);

            roomMapBuilder?.BuildRoomMap(gridStringBuilder, room, viewPoint, keyType, leftMargin, lastY + linePadding, out _, out lastY);

            if (room.Items.Any())
                gridStringBuilder.DrawWrapped(room.Examine().Description.EnsureFinishedSentence(), 2, lastY + linePadding, availableWidth, TextColor, out _, out lastY);
            else
                gridStringBuilder.DrawWrapped("There are no items in this area.", 2, lastY + linePadding, availableWidth, TextColor, out _, out lastY);

            var characterString = SceneHelper.CreateNPCString(room);

            if (!string.IsNullOrEmpty(characterString))
            {
                gridStringBuilder.DrawWrapped(characterString, 2, lastY + linePadding, availableWidth, TextColor, out _, out lastY);
            }

            if (viewPoint.Any)
            {
                var view = SceneHelper.CreateViewpointAsString(room, viewPoint);

                if (!string.IsNullOrEmpty(view))
                    gridStringBuilder.DrawWrapped(view, leftMargin, lastY + linePadding, availableWidth, TextColor, out _, out lastY);
            }

            if (player.Items.Any())
            {
                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, BorderColor);
                gridStringBuilder.DrawWrapped("You have: " + StringUtilities.ConstructExaminablesAsSentence(player.Items.Cast<IExaminable>().ToArray()), leftMargin, lastY + 4, availableWidth, TextColor, out _, out lastY);
            }

            if (contextualCommands?.Any() ?? false)
            {
                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, BorderColor);
                gridStringBuilder.DrawWrapped("You can:", leftMargin, lastY + 4, availableWidth, CommandsColor, out _, out lastY);

                var messageLines = gridStringBuilder.GetNumberOfLines(message, leftMargin, 0, availableWidth);
                var requiredSpaceForMessageAndPrompt = !string.IsNullOrEmpty(message) ? messageLines + 8 : 4;
                var maxCommandLength = contextualCommands.Max(x => x.Command.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                foreach (var contextualCommand in contextualCommands)
                {
                    gridStringBuilder.DrawWrapped(contextualCommand.Command, leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description, descriptionStartX, lastY, availableWidth, CommandsColor, out _, out lastY);

                    // only continue if not run out of space
                    if (lastY + requiredSpaceForMessageAndPrompt >= availableHeight)
                    {
                        gridStringBuilder.DrawWrapped("...", leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, BorderColor);
                gridStringBuilder.DrawWrapped(message.EnsureFinishedSentence(), leftMargin, lastY + 4, availableWidth, TextColor, out _, out _);
            }

            gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
            gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 4, availableHeight, BackgroundColor);
        }

        #endregion
    }
}
