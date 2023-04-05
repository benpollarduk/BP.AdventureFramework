using System.Collections.Generic;
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
        public Dictionary<string, string> CommandsDictionary
        {
            get { return commandsDictionary; }
            set { commandsDictionary = value; }
        }

        /// <summary>
        /// Get or set the commands and descriptions to display to the user
        /// </summary>
        private Dictionary<string, string> commandsDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Get the title
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Get or set the title
        /// </summary>
        private string title;

        /// <summary>
        /// Get the description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Get or set the description
        /// </summary>
        private string description;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the HelpFrame class
        /// </summary>
        public HelpFrame()
        {
            // no input
            AcceptsInput = false;

            // do not show cursor
            ShowCursor = false;

            // set title
            Title = "Help";

            // set decription
            Description = "Provides help for in game commands";

            // add commands
            CommandsDictionary.Add("About", "View information about the games creator");
            CommandsDictionary.Add("space1", "");
            CommandsDictionary.Add("CommandsOn / CommandsOff", "Turn commands on/off");
            CommandsDictionary.Add("KeyOn / KeyOff", "Turn the key on/off");
            CommandsDictionary.Add("Invert", "Invert foreground/background colours");
            CommandsDictionary.Add("Map", "View a map of the current region");
            CommandsDictionary.Add("space2", "");
            CommandsDictionary.Add("Exit", "Exit the game");
            CommandsDictionary.Add("Load", "Load the saved game");
            CommandsDictionary.Add("New", "Start a new game");
            CommandsDictionary.Add("Save", "Save the current game");
            CommandsDictionary.Add("space3", "");
            CommandsDictionary.Add("SoundOff", "Turn sounds off");
            CommandsDictionary.Add("SoundOn", "Turn sounds on");
        }

        /// <summary>
        /// Initializes a new instance of the HelpFrame class
        /// </summary>
        /// <param name="title">Specify the frames title</param>
        /// <param name="description">Specify the frames description</param>
        /// <param name="commands">Sepcify the commands and the descriptions to display</param>
        public HelpFrame(string title, string description, Dictionary<string, string> commands)
        {
            // no input
            AcceptsInput = false;

            // do not show cursor
            ShowCursor = false;

            // set title
            Title = title;

            // set decription
            Description = description;

            // add commands
            CommandsDictionary = commands;
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
            var builder = new StringBuilder();

            // create top
            builder.Append(drawer.ConstructDevider(width));

            // add message
            builder.Append(drawer.ConstructCentralisedString(Title, width));

            // create devider
            builder.Append(drawer.ConstructDevider(width));

            // add message
            builder.Append(drawer.ConstructCentralisedString(Description, width));

            // create devider
            builder.Append(drawer.ConstructDevider(width));

            // add commands title
            builder.Append(drawer.ConstructWrappedPaddedString("GENERAL COMMANDS", width, false));

            // add space
            builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width, false));

            // itterate keys
            foreach (var key in CommandsDictionary.Keys)
                // if a key and a description
                if (!string.IsNullOrEmpty(key) &&
                    !string.IsNullOrEmpty(CommandsDictionary[key]))
                    // add key and description
                    builder.Append(drawer.ConstructWrappedPaddedString(string.Format("{0}{1}- {2}", key, drawer.ConstructWhitespaceString(30 - key.Length), CommandsDictionary[key]), width, false));
                else if (!string.IsNullOrEmpty(key) &&
                         string.IsNullOrEmpty(CommandsDictionary[key]))
                    // add empty
                    builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                else
                    // add empty
                    builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, height - (drawer.DetermineLinesInString(builder.ToString()) + 7)));

            // add commands title
            builder.Append(drawer.ConstructWrappedPaddedString("Press Enter to return to the game", width, true));

            // add buffer
            builder.Append(drawer.ConstructPaddedArea(width, 4));

            // create devider
            var devider = drawer.ConstructDevider(width);

            // add devider removing the last \n
            builder.Append(devider.Remove(devider.Length - 1));

            // return builder
            return builder.ToString();
        }

        #endregion
    }
}