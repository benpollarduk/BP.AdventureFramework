using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying help
    /// </summary>
    public class HelpFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the commands and descriptions to display to the user
        /// </summary>
        public Dictionary<String, String> CommandsDictionary
        {
            get { return this.commandsDictionary; }
            set { this.commandsDictionary = value; }
        }

        /// <summary>
        /// Get or set the commands and descriptions to display to the user
        /// </summary>
        private Dictionary<String, String> commandsDictionary = new Dictionary<String, String>();

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
        /// Initializes a new instance of the HelpFrame class
        /// </summary>
        public HelpFrame()
        {
            // no input
            this.AcceptsInput = false;

            // do not show cursor
            this.ShowCursor = false;

            // set title
            this.Title = "Help";

            // set decription
            this.Description = "Provides help for in game commands";

            // add commands
            this.CommandsDictionary.Add("About", "View information about the games creator");
            this.CommandsDictionary.Add("space1", "");
            this.CommandsDictionary.Add("CommandsOn / CommandsOff", "Turn commands on/off");
            this.CommandsDictionary.Add("KeyOn / KeyOff", "Turn the key on/off");
            this.CommandsDictionary.Add("Invert", "Invert foreground/background colours");
            this.CommandsDictionary.Add("Map", "View a map of the current region");
            this.CommandsDictionary.Add("space2", "");
            this.CommandsDictionary.Add("Exit", "Exit the game");
            this.CommandsDictionary.Add("Load", "Load the saved game");
            this.CommandsDictionary.Add("New", "Start a new game");
            this.CommandsDictionary.Add("Save", "Save the current game");
            this.CommandsDictionary.Add("space3", "");
            this.CommandsDictionary.Add("SoundOff", "Turn sounds off");
            this.CommandsDictionary.Add("SoundOn", "Turn sounds on");
        }

        /// <summary>
        /// Initializes a new instance of the HelpFrame class
        /// </summary>
        /// <param name="title">Specify the frames title</param>
        /// <param name="description">Specify the frames description</param>
        /// <param name="commands">Sepcify the commands and the descriptions to display</param>
        public HelpFrame(String title, String description, Dictionary<String,String> commands)
        {
            // no input
            this.AcceptsInput = false;

            // do not show cursor
            this.ShowCursor = false;

            // set title
            this.Title = title;

            // set decription
            this.Description = description;

            // add commands
            this.CommandsDictionary = commands;
        }

        /// <summary>
        /// Build this HelpFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // create builder
            StringBuilder builder = new StringBuilder();

            // create top
            builder.Append(drawer.ConstructDevider(width));

            // add message
            builder.Append(drawer.ConstructCentralisedString(this.Title, width));

            // create devider
            builder.Append(drawer.ConstructDevider(width));

            // add message
            builder.Append(drawer.ConstructCentralisedString(this.Description, width));

            // create devider
            builder.Append(drawer.ConstructDevider(width));

            // add commands title
            builder.Append(drawer.ConstructWrappedPaddedString("GENERAL COMMANDS", width, false));

            // add space
            builder.Append(drawer.ConstructWrappedPaddedString(String.Empty, width, false));

            // itterate keys
            foreach (String key in this.CommandsDictionary.Keys)
            {
                // if a key and a description
                if ((!String.IsNullOrEmpty(key)) &&
                    (!String.IsNullOrEmpty(this.CommandsDictionary[key])))
                {
                    // add key and description
                    builder.Append(drawer.ConstructWrappedPaddedString(String.Format("{0}{1}- {2}", key, drawer.ConstructWhitespaceString(30 - key.Length), this.CommandsDictionary[key]), width, false));
                }
                else if ((!String.IsNullOrEmpty(key)) &&
                         (String.IsNullOrEmpty(this.CommandsDictionary[key])))
                {
                    // add empty
                    builder.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));
                }
                else
                {
                    // add empty
                    builder.Append(drawer.ConstructWrappedPaddedString(String.Empty, width));
                }
            }

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, height - (drawer.DetermineLinesInString(builder.ToString()) + 7)));

            // add commands title
            builder.Append(drawer.ConstructWrappedPaddedString("Press Enter to return to the game", width, true));

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, 4));

            // create devider
            String devider = drawer.ConstructDevider(width);

            // add devider removing the last \n
            builder.Append(devider.Remove(devider.Length - 1));

            // return builder
            return builder.ToString();
        }

        #endregion
    }
}
