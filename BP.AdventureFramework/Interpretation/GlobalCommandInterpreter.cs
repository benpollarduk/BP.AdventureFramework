using BP.AdventureFramework.Commands.Global;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting global commands.
    /// </summary>
    internal class GlobalCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the about command.
        /// </summary>
        public const string About = "About";

        /// <summary>
        /// Get the exit command.
        /// </summary>
        public const string Exit = "Exit";

        /// <summary>
        /// Get the help command.
        /// </summary>
        public const string Help = "Help";

        /// <summary>
        /// Get the map command.
        /// </summary>
        public const string Map = "Map";

        /// <summary>
        /// Get the new command.
        /// </summary>
        public const string New = "New";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        {
            new CommandHelp(About, "View information about the games creator"),
            new CommandHelp(Map, "View a map of the current region"),
            new CommandHelp(Exit, "Exit the game"),
            new CommandHelp(New, "Start a new game")
        };

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            if (input.InsensitiveEquals(About))
                return new InterpretationResult(true, new About());

            if (input.InsensitiveEquals(Exit))
                return new InterpretationResult(true, new Exit());

            if (input.InsensitiveEquals(Help))
                return new InterpretationResult(true, new Help());

            if (input.InsensitiveEquals(Map))
                return new InterpretationResult(true, new Map());

            if (input.InsensitiveEquals(New))
                return new InterpretationResult(true, new New());

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            return new CommandHelp[0];
        }

        #endregion
    }
}
