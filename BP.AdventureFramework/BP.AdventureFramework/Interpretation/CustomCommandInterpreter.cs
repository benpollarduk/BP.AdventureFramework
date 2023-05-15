using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting custom commands.
    /// </summary>
    public class CustomCommandInterpreter : IInterpreter
    {
        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = null;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            var commands = new List<CustomCommand>();

            foreach (var examinable in game.GetAllPlayerVisibleExaminables().Where(x => x.Commands != null))
                commands.AddRange(examinable.Commands);

            var command = commands.FirstOrDefault(x => x.Help.Command.Equals(input, StringComparison.CurrentCultureIgnoreCase));

            return command == null ? InterpretationResult.Fail : new InterpretationResult(true, command);
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            if (game.ActiveConverser?.Conversation != null)
                return new CommandHelp[0];

            var help = new List<CommandHelp>();

            foreach (var examinable in game.GetAllPlayerVisibleExaminables().Where(x => x.Commands != null)) 
                help.AddRange(examinable.Commands.Select(command => command.Help));

            return help.ToArray();
        }

        #endregion
    }
}
