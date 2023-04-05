using System;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying a command based interface
    /// </summary>
    public abstract class Frame : IDisposable
    {
        #region IDisposable Members

        /// <summary>
        /// Dispose this Frame
        /// </summary>
        public void Dispose()
        {
            OnDisposed();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the required cursors left position
        /// </summary>
        public int CursorLeft
        {
            get { return cursorLeft; }
            protected set { cursorLeft = value; }
        }

        /// <summary>
        /// Get or set the required cursors left position
        /// </summary>
        private int cursorLeft;

        /// <summary>
        /// Get the required cursors top position
        /// </summary>
        public int CursorTop
        {
            get { return cursorTop; }
            protected set { cursorTop = value; }
        }

        /// <summary>
        /// Get or set the required cursors top position
        /// </summary>
        private int cursorTop;

        /// <summary>
        /// Get or set if the cursor should be shown
        /// </summary>
        public bool ShowCursor
        {
            get { return showCursor; }
            set { showCursor = value; }
        }

        /// <summary>
        /// Get or set if the cursor should be shown
        /// </summary>
        private bool showCursor = true;

        /// <summary>
        /// Get or set if this Frame excepts input
        /// </summary>
        public bool AcceptsInput
        {
            get { return acceptsInput; }
            set { acceptsInput = value; }
        }

        /// <summary>
        /// Get or set if this Frame excepts input
        /// </summary>
        private bool acceptsInput = true;

        /// <summary>
        /// Occurs if this Frame becomes invalid
        /// </summary>
        public event FrameEventHandler Invalidated;

        #endregion

        #region Methods

        /// <summary>
        /// Build this Frame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public virtual string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invalidate this Frame, forcing a re-render
        /// </summary>
        protected virtual void Invalidate()
        {
            // if event handlers 
            if (Invalidated != null)
                // dispatch event
                Invalidated(this, new FrameEventArgs(this));
        }

        /// <summary>
        /// Handle disposal of this Frame
        /// </summary>
        protected virtual void OnDisposed()
        {
        }

        #endregion
    }
}