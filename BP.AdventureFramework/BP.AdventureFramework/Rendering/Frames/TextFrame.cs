using System;
using System.IO;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Provides a simple text based frame for displaying a command based interface.
    /// </summary>
    public sealed class TextFrame : IFrame
    {
        #region Fields

        private readonly string frameData;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TextFrame class.
        /// </summary>
        /// <param name="frameData">The data the frame provides.</param>
        /// <param name="cursorLeft">The cursor left position.</param>
        /// <param name="cursorTop">The cursor top position.</param>
        public TextFrame(string frameData, int cursorLeft, int cursorTop)
        {
            this.frameData = frameData;
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
        }

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.frameData;
        }

        #endregion

        #region Implementation of IFrame

        /// <summary>
        /// Get the cursor left position.
        /// </summary>
        public int CursorLeft { get; }

        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        public int CursorTop { get; }

        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Get or set if this Frame excepts input.
        /// </summary>
        public bool AcceptsInput { get; set; } = true;

        /// <summary>
        /// Occurs when this frame is updated.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Render this frame on a writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void Render(TextWriter writer)
        {
            writer.Write(this);
        }

        #endregion
    }
}