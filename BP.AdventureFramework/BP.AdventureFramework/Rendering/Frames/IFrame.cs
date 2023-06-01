using System;
using System.IO;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents any object that is a frame that can display a command based interface.
    /// </summary>
    public interface IFrame
    {
        /// <summary>
        /// Get the cursor left position.
        /// </summary>
        int CursorLeft { get; }
        /// <summary>
        /// Get the cursor top position.
        /// </summary>
        int CursorTop { get; }
        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        bool ShowCursor { get; set; }
        /// <summary>
        /// Get or set if this Frame excepts input.
        /// </summary>
        bool AcceptsInput { get; set; }
        /// <summary>
        /// Occurs when this frame is updated.
        /// </summary>
        event EventHandler Updated;
        /// <summary>
        /// Render this frame on a writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        void Render(TextWriter writer);
    }
}
