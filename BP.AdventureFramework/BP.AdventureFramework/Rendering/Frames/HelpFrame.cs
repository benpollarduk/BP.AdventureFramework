using System.Collections.Generic;
using System.Text;
using BP.AdventureFramework.Interpretation;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying help.
    /// </summary>
    internal sealed class HelpFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the commands and descriptions to display to the user.
        /// </summary>
        public Dictionary<string, string> CommandsDictionary { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Get or set the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the HelpFrame class.
        /// </summary>
        public HelpFrame()
        {
            AcceptsInput = false;
            ShowCursor = false;
            Title = "Help";
            Description = "Provides help for in game commands";

            CommandsDictionary.Add(GlobalCommandInterpreter.About, "View information about the games creator");
            CommandsDictionary.Add("space1", "");
            CommandsDictionary.Add($"{FrameCommandInterpreter.CommandsOn} / {FrameCommandInterpreter.CommandsOff}", "Turn commands on/off");
            CommandsDictionary.Add($"{FrameCommandInterpreter.KeyOn}  / {FrameCommandInterpreter.KeyOff} ", "Turn the key on/off");
            CommandsDictionary.Add(GlobalCommandInterpreter.Map, "View a map of the current region");
            CommandsDictionary.Add("space2", "");
            CommandsDictionary.Add(GlobalCommandInterpreter.Exit, "Exit the game");
            CommandsDictionary.Add(GlobalCommandInterpreter.New, "Start a new game");
        }

        /// <summary>
        /// Initializes a new instance of the HelpFrame class.
        /// </summary>
        /// <param name="title">Specify the frames title.</param>
        /// <param name="description">Specify the frames description.</param>
        /// <param name="commands">Specify the commands and the descriptions to display.</param>
        public HelpFrame(string title, string description, Dictionary<string, string> commands)
        {
            AcceptsInput = false;
            ShowCursor = false;
            Title = title;
            Description = description;
            CommandsDictionary = commands;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this HelpFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var builder = new StringBuilder();
            builder.Append(drawer.ConstructDivider(width));
            builder.Append(drawer.ConstructCentralisedString(Title, width));
            builder.Append(drawer.ConstructDivider(width));
            builder.Append(drawer.ConstructCentralisedString(Description, width));
            builder.Append(drawer.ConstructDivider(width));
            builder.Append(drawer.ConstructWrappedPaddedString("GENERAL COMMANDS", width, false));
            builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width, false));

            foreach (var key in CommandsDictionary.Keys)
            {
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(CommandsDictionary[key]))
                    builder.Append(drawer.ConstructWrappedPaddedString($"{key}{drawer.ConstructWhitespaceString(30 - key.Length)}- {CommandsDictionary[key]}", width, false));
                else if (!string.IsNullOrEmpty(key) && string.IsNullOrEmpty(CommandsDictionary[key]))
                    builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
                else
                    builder.Append(drawer.ConstructWrappedPaddedString(string.Empty, width));
            }

            builder.Append(drawer.ConstructPaddedArea(width, height - (drawer.DetermineLinesInString(builder.ToString()) + 7)));
            builder.Append(drawer.ConstructWrappedPaddedString("Press Enter to return to the game", width, true));
            builder.Append(drawer.ConstructPaddedArea(width, 4));
            var divider = drawer.ConstructDivider(width);
            builder.Append(divider.Remove(divider.Length - 1));
            return builder.ToString();
        }

        #endregion
    }
}