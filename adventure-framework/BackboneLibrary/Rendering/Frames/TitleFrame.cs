using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame that can be used as a title screen
    /// </summary>
    public class TitleFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get the title
        /// </summary>
        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Get or set the title
        /// </summary>
        private String title;

        /// <summary>
        /// Get the description
        /// </summary>
        public String Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Get or set the description
        /// </summary>
        private String description;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the TitleFrame class
        /// </summary>
        protected TitleFrame()
        {
            // as default don't show
            this.ShowCursor = false;

            // as default doesn't take input
            this.AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the TitleFrame class
        /// </summary>
        /// <param name="title">The title of the game</param>
        /// <param name="description">A description of the game</param>
        public TitleFrame(String title, String description)
        {
            // as default don't show
            this.ShowCursor = false;

            // as default doesn't take input
            this.AcceptsInput = false;

            // set title
            this.Title = title;

            // set description
            this.Description = description;
        }

        /// <summary>
        /// Build this TitleFrame into a text based display
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
            constructedScene += drawer.ConstructWrappedPaddedString(this.Title, width, true);

            // add another devider
            constructedScene += devider;

            // add name
            constructedScene += drawer.ConstructWrappedPaddedString(this.Description, width, true);

            // add another devider
            constructedScene += devider;

            // add padded area
            constructedScene += drawer.ConstructPaddedArea(width, (height / 2) - drawer.DetermineLinesInString(constructedScene));

            // add command
            constructedScene += drawer.ConstructWrappedPaddedString("Press Enter to start", width, true);

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
