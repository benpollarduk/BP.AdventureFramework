using System;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Event arguments for Frame events.
    /// </summary>
    public class FrameEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Get the frame.
        /// </summary>
        public Frame Frame { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameEventArgs class.
        /// </summary>
        /// <param name="frame">The Frame to specify for these arguments.</param>
        public FrameEventArgs(Frame frame)
        {
            Frame = frame;
        }

        #endregion
    }
}