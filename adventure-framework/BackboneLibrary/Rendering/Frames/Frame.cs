using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying a command based interface
    /// </summary>
    public abstract class Frame : IDisposable
    {
        #region Properties

        /// <summary>
        /// Get the required cursors left position
        /// </summary>
        public Int32 CursorLeft
        {
            get { return this.cursorLeft; }
            protected set { this.cursorLeft = value; }
        }

        /// <summary>
        /// Get or set the required cursors left position
        /// </summary>
        private Int32 cursorLeft = 0;

        /// <summary>
        /// Get the required cursors top position
        /// </summary>
        public Int32 CursorTop
        {
            get { return this.cursorTop; }
            protected set { this.cursorTop = value; }
        }

        /// <summary>
        /// Get or set the required cursors top position
        /// </summary>
        private Int32 cursorTop = 0;

        /// <summary>
        /// Get or set if the cursor should be shown
        /// </summary>
        public Boolean ShowCursor
        {
            get { return this.showCursor; }
            set { this.showCursor = value; }
        }

        /// <summary>
        /// Get or set if the cursor should be shown
        /// </summary>
        private Boolean showCursor = true;

        /// <summary>
        /// Get or set if this Frame excepts input
        /// </summary>
        public Boolean AcceptsInput
        {
            get { return this.acceptsInput; }
            set { this.acceptsInput = value; }
        }

        /// <summary>
        /// Get or set if this Frame excepts input
        /// </summary>
        private Boolean acceptsInput = true;

        /// <summary>
        /// Occurs if this Frame becomes invalid
        /// </summary>
        public event FrameEventHandler Invalidated;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Frame class
        /// </summary>
        protected Frame()
        {
        }

        /// <summary>
        /// Build this Frame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public virtual String BuildFrame(Int32 width, Int32 height, FrameDrawer drawer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invalidate this Frame, forcing a re-render
        /// </summary>
        protected virtual void Invalidate()
        {
            // if event handlers 
            if (this.Invalidated != null)
            {
                // dispatch event
                this.Invalidated(this, new FrameEventArgs(this));
            }
        }

        /// <summary>
        /// Handle disposal of this Frame
        /// </summary>
        protected virtual void OnDisposed()
        {
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose this Frame
        /// </summary>
        public void Dispose()
        {
            this.OnDisposed();
        }

        #endregion
    }
}
