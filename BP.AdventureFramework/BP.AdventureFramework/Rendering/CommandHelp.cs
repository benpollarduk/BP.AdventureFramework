using BP.AdventureFramework.Interpretation;

namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// Provides help for a command.
    /// </summary>
    public class CommandHelp
    {
        #region StaticProperties

        /// <summary>
        /// Get a spacer.
        /// </summary>
        public static CommandHelp Spacer { get; } = new CommandHelp(string.Empty, string.Empty);

        /// <summary>
        /// Get help for the About command.
        /// </summary>
        public static CommandHelp About { get; } = new CommandHelp(GlobalCommandInterpreter.About, "View information about the games creator");

        /// <summary>
        /// Get help for the CommandsOn / CommandsOff command.
        /// </summary>
        public static CommandHelp CommandsOnCommandsOff { get; } = new CommandHelp($"{FrameCommandInterpreter.CommandsOn} / {FrameCommandInterpreter.CommandsOff}", "Turn commands on/off");

        /// <summary>
        /// Get help for the KeyOn / KeyOff command.
        /// </summary>
        public static CommandHelp KeyOnKeyOff { get; } = new CommandHelp($"{FrameCommandInterpreter.KeyOn}  / {FrameCommandInterpreter.KeyOff} ", "Turn the key on/off");

        /// <summary>
        /// Get help for the Map command.
        /// </summary>
        public static CommandHelp Map { get; } = new CommandHelp(GlobalCommandInterpreter.Map, "View a map of the current region");

        /// <summary>
        /// Get help for the Exit command.
        /// </summary>
        public static CommandHelp Exit { get; } = new CommandHelp(GlobalCommandInterpreter.Exit, "Exit the game");

        /// <summary>
        /// Get help for the New command.
        /// </summary>
        public static CommandHelp New { get; } = new CommandHelp(GlobalCommandInterpreter.New, "Start a new game");

        #endregion

        #region Properties

        /// <summary>
        /// Get the command.
        /// </summary>
        public string Command { get; }

        /// <summary>
        /// Get the description of the command.
        /// </summary>
        public string Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CommandHelp class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">The help.</param>
        public CommandHelp(string command, string description)
        {
            Command = command;
            Description = description;
        }

        #endregion
    }
}
