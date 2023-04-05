using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Locations;
using AdventureFramework.Interaction;
using AdventureFramework.Rendering.Frames;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// A class for constructing and drawing Frame's
    /// </summary>
    public class FrameDrawer : Drawer
    {
        #region Properties

        /// <summary>
        /// Get or set the screen devider character
        /// </summary>
        public Char DeviderCharacter
        {
            get { return this.deviderCharacter; }
            set { this.deviderCharacter = value; }
        }

        /// <summary>
        /// Get or set the screen devider character
        /// </summary>
        private Char deviderCharacter = Convert.ToChar("=");

        /// <summary>
        /// Get or set if commands are displayed
        /// </summary>
        public Boolean DisplayCommands
        {
            get { return this.displayCommands; }
            set { this.displayCommands = value; }
        }

        /// <summary>
        /// Get or set if commands are displayed
        /// </summary>
        private Boolean displayCommands = true;

        /// <summary>
        /// Occurs when a special frame has been requested to be displayed
        /// </summary>
        public static event FrameEventHandler DisplayedSpecialFrame;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the SceneDrawer class
        /// </summary>
        public FrameDrawer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SceneDrawer class
        /// </summary>
        /// <param name="leftBoundaryCharacter">The character to use for left boundaries</param>
        /// <param name="rightBoundaryCharacter">The character to use for right boundaries</param>
        /// <param name="devidingCharacter">The character to use for deviders</param>
        public FrameDrawer(Char leftBoundaryCharacter, Char rightBoundaryCharacter, Char devidingCharacter)
        {
            // set
            this.LeftBoundaryCharacter = leftBoundaryCharacter;

            // set
            this.RightBoundaryCharacter = rightBoundaryCharacter;

            // set
            this.DeviderCharacter = devidingCharacter;
        }

        /// <summary>
        /// Construct a deviding horizontal line
        /// </summary>
        /// <param name="width">The width of the devider</param>
        /// <returns>A constructed devider</returns>
        public virtual String ConstructDevider(Int32 width)
        {
            return this.ConstructDevider(width, this.LeftBoundaryCharacter, this.DeviderCharacter, this.RightBoundaryCharacter);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Request a custom Frame to be displayed to any context listening for the FrameDrawer.DisplaySpecialFrame event
        /// </summary>
        /// <param name="frame">The frame to </param>
        public static void DisplaySpecialFrame(Frame frame)
        {
            // if some listener
            if (FrameDrawer.DisplayedSpecialFrame != null)
            {
                // redraw
                FrameDrawer.DisplayedSpecialFrame(null, new FrameEventArgs(frame));
            }
        }

        #endregion
    }

    /// <summary>
    /// Enumeration of key types
    /// </summary>
    public enum EKeyType
    {
        /// <summary>
        /// No key
        /// </summary>
        None = 0,
        /// <summary>
        /// Full key
        /// </summary>
        Full,
        /// <summary>
        /// Dynamic key, only show relevant key items
        /// </summary>
        Dynamic
    }

    /// <summary>
    /// Enumeration of FrameDrawer options
    /// </summary>
    public enum EFrameDrawerOption
    {
        /// <summary>
        /// Turn the key on
        /// </summary>
        KeyOn = 0,
        /// <summary>
        /// Turn the key off
        /// </summary>
        KeyOff,
        /// <summary>
        /// Turn commands on
        /// </summary>
        CommandsOn,
        /// <summary>
        /// Turn commands off
        /// </summary>
        CommandsOff,
        /// <summary>
        /// Invert display colours
        /// </summary>
        Invert
    }
}
