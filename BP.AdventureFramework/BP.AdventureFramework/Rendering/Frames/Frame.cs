using System;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying a command based interface.
    /// </summary>
    public abstract class Frame : IDisposable
    {
        #region Properties

        /// <summary>
        /// Get the required cursors left position.
        /// </summary>
        public int CursorLeft { get; protected set; }

        /// <summary>
        /// Get the required cursors top position.
        /// </summary>
        public int CursorTop { get; protected set; }

        /// <summary>
        /// Get or set if the cursor should be shown.
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Get or set if this Frame excepts input.
        /// </summary>
        public bool AcceptsInput { get; set; } = true;

        /// <summary>
        /// Occurs if this Frame becomes invalid.
        /// </summary>
        public event FrameEventHandler Invalidated;

        #endregion

        #region Methods

        /// <summary>
        /// Build this Frame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public virtual string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invalidate this Frame, forcing a re-render.
        /// </summary>
        protected virtual void Invalidate()
        {
            Invalidated?.Invoke(this, new FrameEventArgs(this));
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }

        #endregion
    }
}