using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using AdventureFramework.Structure;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for when the game ends
    /// </summary>
    public class EndFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the message
        /// </summary>
        public String Message
        {
            get { return this.message; }
            protected set { this.message = value; }
        }

        /// <summary>
        /// Get or set the message
        /// </summary>
        private String message;

        /// <summary>
        /// Get the reason
        /// </summary>
        public String Reason
        {
            get { return this.reason; }
            protected set { this.reason = value; }
        }

        /// <summary>
        /// Get or set the reason
        /// </summary>
        private String reason;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the EndFrame class
        /// </summary>
        protected EndFrame()
        {
            // as default don't show
            this.ShowCursor = false;

            // as default doesn't take input
            this.AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the EndFrame class
        /// </summary>
        /// <param name="message">A message to show the user</param>
        /// <param name="reason">The reason for the end</param>
        public EndFrame(String message, String reason)
        {
            // as default don't show
            this.ShowCursor = false;

            // as default doesn't take input
            this.AcceptsInput = false;

            // set message
            this.Message = message;

            // set reason
            this.Reason = reason;
        }

        /// <summary>
        /// Build this EndFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // construct a devider
            String devider = drawer.ConstructDevider(width);

            // construct devider
            String constructedScene = devider;

            // add name
            constructedScene += drawer.ConstructWrappedPaddedString(this.Message, width, true);

            // add another devider
            constructedScene += devider;

            // add name
            constructedScene += drawer.ConstructWrappedPaddedString(this.Reason, width, true);

            // add another devider
            constructedScene += devider;

            // add padded area
            constructedScene += drawer.ConstructPaddedArea(width, (height / 2) - drawer.DetermineLinesInString(constructedScene));

            // add command
            constructedScene += drawer.ConstructWrappedPaddedString("Press Enter to return to title screen", width, true);

            // add padded area
            constructedScene += drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(constructedScene) - 2);

            // add devider removing the last \n
            constructedScene += devider.Remove(devider.Length - 1);

            // return construction
            return constructedScene;
        }

        #endregion
    }
}
