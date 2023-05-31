using System;
using BP.AdventureFramework.Commands.Frame;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting frame commands.
    /// </summary>
    internal class FrameCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the commands off command.
        /// </summary>
        public const string CommandsOff = "CommandsOff";

        /// <summary>
        /// Get the commands on command.
        /// </summary>
        public const string CommandsOn = "CommandsOn";

        /// <summary>
        /// Get the keys on command.
        /// </summary>
        public const string KeyOn = "KeyOn";

        /// <summary>
        /// Get the commands off command.
        /// </summary>
        public const string KeyOff = "KeyOff";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        {
            new CommandHelp($"{CommandsOn} / {CommandsOff}", "Turn commands on/off"),
            new CommandHelp($"{KeyOn} / {KeyOff} ", "Turn the key on/off")
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
            if (input.InsensitiveEquals(CommandsOff))
                return new InterpretationResult(true, new CommandsOff());

            if (input.InsensitiveEquals(CommandsOn))
                return new InterpretationResult(true, new CommandsOn());

            if (input.InsensitiveEquals(KeyOff))
                return new InterpretationResult(true, new KeyOff());

            if (input.InsensitiveEquals(KeyOn))
                return new InterpretationResult(true, new KeyOn());

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
