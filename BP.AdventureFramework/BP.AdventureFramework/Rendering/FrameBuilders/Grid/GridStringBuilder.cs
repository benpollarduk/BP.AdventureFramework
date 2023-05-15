using System;
using System.Drawing;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Grid
{
    /// <summary>
    /// Provides a class for building strings, as apart of a grid.
    /// </summary>
    public class GridStringBuilder
    {
        #region Fields

        private ConsoleColor[,] colors;
        private char[,] buffer;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for left boundary.
        /// </summary>
        public char LeftBoundaryCharacter { get; set; }

        /// <summary>
        /// Get or set the character used for right boundary.
        /// </summary>
        public char RightBoundaryCharacter { get; set; }

        /// <summary>
        /// Get or set the character used for horizontal dividers.
        /// </summary>
        public char HorizontalDividerCharacter { get; set; }

        /// <summary>
        /// Get or set the line terminator.
        /// </summary>
        public string LineTerminator { get; set; } = Environment.NewLine;

        /// <summary>
        /// Get the display size.
        /// </summary>
        public Size DisplaySize { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GridStringBuilder class.
        /// </summary>
        /// <param name="leftBoundaryCharacter">The character to use for left boundaries.</param>
        /// <param name="rightBoundaryCharacter">The character to use for right boundaries.</param>
        /// <param name="horizontalDividerCharacter">The character to use for horizontal dividers.</param>
        public GridStringBuilder(char leftBoundaryCharacter = (char)124, char rightBoundaryCharacter = (char)124, char horizontalDividerCharacter = (char)45)
        {
            LeftBoundaryCharacter = leftBoundaryCharacter;
            RightBoundaryCharacter = rightBoundaryCharacter;
            HorizontalDividerCharacter = horizontalDividerCharacter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resize this builder.
        /// </summary>
        /// <param name="displaySize">The new size.</param>
        public void Resize(Size displaySize)
        {
            DisplaySize = displaySize;
            colors = new ConsoleColor[displaySize.Width, displaySize.Height];
            Flush();
        }

        /// <summary>
        /// Get a character from the buffer.
        /// </summary>
        /// <param name="x">The x position of the character.</param>
        /// <param name="y">The y position of the character.</param>
        /// <returns>The character.</returns>
        public char GetCharacter(int x, int y)
        {
            return buffer[x, y];
        }

        /// <summary>
        /// Get a color for a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <returns>The cell color.</returns>
        public ConsoleColor GetCellColor(int x, int y)
        {
            return colors[x, y];
        }

        /// <summary>
        /// Flush the buffer.
        /// </summary>
        public void Flush()
        {
            buffer = new char[DisplaySize.Width, DisplaySize.Height];
        }

        /// <summary>
        /// Set a cell.
        /// </summary>
        /// <param name="x">The x position of the cell.</param>
        /// <param name="y">The y position of the cell.</param>
        /// <param name="character">The character.</param>
        /// <param name="color">The color of the character.</param>
        public void SetCell(int x, int y, char character, ConsoleColor color)
        {
            buffer[x, y] = character;
            colors[x, y] = color;
        }

        /// <summary>
        /// Draw the bound
        /// </summary>
        /// <param name="color">The color to draw the boundary.</param>
        public void DrawBoundary(ConsoleColor color)
        {
            DrawHorizontalDivider(0, color);
            DrawHorizontalDivider(DisplaySize.Height - 1, color);

            for (var i = 0; i < DisplaySize.Height; i++)
            {
                SetCell(0, i, LeftBoundaryCharacter, color);
                SetCell(DisplaySize.Width - 1, i, RightBoundaryCharacter, color);
            }
        }

        /// <summary>
        /// Draw a horizontal divider.
        /// </summary>
        /// <param name="y">The y position of the divider.</param>
        /// <param name="color">The color to draw the boundary.</param>
        /// <returns>The divider.</returns>
        public void DrawHorizontalDivider(int y, ConsoleColor color)
        {
            for (var i = 1; i < DisplaySize.Width - 1; i++)
                SetCell(i, y, HorizontalDividerCharacter, color);
        }

        /// <summary>
        /// Draw a wrapped string.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startX">The start x position.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <param name="color">The color to draw the text.</param>
        /// <param name="endX">The end x position.</param>
        /// <param name="endY">The end y position.</param>
        public void DrawWrapped(string value, int startX, int startY, int maxWidth, ConsoleColor color, out int endX, out int endY)
        {
            endX = startX;
            endY = startY;

            while (value.Length > 0)
            {
                var chunk = StringUtilities.CutLineFromParagraph(ref value, maxWidth);

                for (var i = 0; i < chunk.Length; i++)
                {
                    endX = startX + i;
                    SetCell(endX, endY, chunk[i], color);
                }

                if (value.Trim().Length > 0 )
                    endY++;
            }
        }

        /// <summary>
        /// Get the number of lines a string will take up.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startX">The start x position.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <returns>The number of lines the string will take up.</returns>
        public int GetNumberOfLines(string value, int startX, int startY, int maxWidth)
        {
            var endY = startY;
            var copy = value.Clone().ToString();

            while (copy.Length > 0)
            {
                StringUtilities.CutLineFromParagraph(ref copy, maxWidth);

                if (copy.Trim().Length > 0)
                    endY++;
            }

            return (endY - startY) + 1;
        }

        /// <summary>
        /// Draw a wrapped string.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <param name="startY">The start y position.</param>
        /// <param name="maxWidth">The max width of the string.</param>
        /// <param name="color">The color to draw the text.</param>
        /// <param name="endX">The end x position.</param>
        /// <param name="endY">The end y position.</param>
        public void DrawCentralisedWrapped(string value, int startY, int maxWidth, ConsoleColor color, out int endX, out int endY)
        {
            endX = 0;
            endY = startY;

            while (value.Length > 0)
            {
                var chunk = StringUtilities.CutLineFromParagraph(ref value, maxWidth);
                var startX = (maxWidth / 2) - (chunk.Length / 2);

                for (var i = 0; i < chunk.Length; i++)
                {
                    endX = startX + i;
                    SetCell(endX, endY, chunk[i], color);
                }

                if (value.Trim().Length > 0)
                    endY++;
            }
        }

        /// <summary>
        /// Draw an underline.
        /// </summary>
        /// <param name="x">The position of the underline, in x.</param>
        /// <param name="y">The position of the underline, in y.</param>
        /// <param name="length">The length of the underline.</param>
        /// <param name="color">The color of the underline.</param>
        public void DrawUnderline(int x, int y, int length, ConsoleColor color)
        {
            var underline = Convert.ToChar("-");

            for (var i = 0; i < length; i++)
                SetCell(x + i, y, underline, color);
        }

        #endregion
    }
}
