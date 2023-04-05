using System;
using AdventureFramework.Rendering.Frames;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// A class for constructing and drawing Frame's
    /// </summary>
    public class FrameDrawer : Drawer
    {
        #region StaticMethods

        /// <summary>
        /// Request a custom Frame to be displayed to any context listening for the FrameDrawer.DisplaySpecialFrame event
        /// </summary>
        /// <param name="frame">The frame to </param>
        public static void DisplaySpecialFrame(Frame frame)
        {
            // if some listener
            if (DisplayedSpecialFrame != null)
                // redraw
                DisplayedSpecialFrame(null, new FrameEventArgs(frame));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the screen devider character
        /// </summary>
        public char DeviderCharacter
        {
            get { return deviderCharacter; }
            set { deviderCharacter = value; }
        }

        /// <summary>
        /// Get or set the screen devider character
        /// </summary>
        private char deviderCharacter = Convert.ToChar("=");

        /// <summary>
        /// Get or set if commands are displayed
        /// </summary>
        public bool DisplayCommands
        {
            get { return displayCommands; }
            set { displayCommands = value; }
        }

        /// <summary>
        /// Get or set if commands are displayed
        /// </summary>
        private bool displayCommands = true;

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
        public FrameDrawer(char leftBoundaryCharacter, char rightBoundaryCharacter, char devidingCharacter)
        {
            // set
            LeftBoundaryCharacter = leftBoundaryCharacter;

            // set
            RightBoundaryCharacter = rightBoundaryCharacter;

            // set
            DeviderCharacter = devidingCharacter;
        }

        /// <summary>
        /// Construct a deviding horizontal line
        /// </summary>
        /// <param name="width">The width of the devider</param>
        /// <returns>A constructed devider</returns>
        public virtual string ConstructDevider(int width)
        {
            return ConstructDevider(width, LeftBoundaryCharacter, DeviderCharacter, RightBoundaryCharacter);
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