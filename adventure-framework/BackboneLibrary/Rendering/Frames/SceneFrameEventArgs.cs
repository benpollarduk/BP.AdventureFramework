using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Event arguments for Frame events
    /// </summary>
    public class FrameEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the frame
        /// </summary>
        public Frame Frame
        {
            get { return this.frame; }
            protected set { this.frame = value; }
        }

        /// <summary>
        /// Get or set the frame
        /// </summary>
        private Frame frame;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FrameEventArgs class
        /// </summary>
        protected FrameEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FrameEventArgs class
        /// </summary>
        /// <param name="frame">The Frame to specify for these arguments</param>
        public FrameEventArgs(Frame frame)
        {
            // set frame
            this.Frame = frame;
        }

        #endregion
    }

    /// <summary>
    /// Event handler for Frame events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void FrameEventHandler(object sender, FrameEventArgs e);
}
