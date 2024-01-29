using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Commands.Game;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting game input.
    /// </summary>
    internal class InputInterpreter : IInterpreter
    {
        #region Properties

        /// <summary>
        /// Get the interpreters.
        /// </summary>
        protected IInterpreter[] Interpreters { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InputInterpreter class.
        /// </summary>
        /// <param name="interpreters">The interpreters.</param>
        public InputInterpreter(params IInterpreter[] interpreters)
        {
            Interpreters = interpreters;
        }

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands
        {
            get
            {
                var l = new List<CommandHelp>();

                foreach (var commands in Interpreters.Select(i => i.SupportedCommands))
                {
                    if (commands != null)
                        l.AddRange(commands);
                }

                return l.ToArray();
            }
        }

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            foreach (var interpreter in Interpreters)
            {
                var result = interpreter.Interpret(input, game);

                if (result.WasInterpretedSuccessfully)
                    return result;
            }

            return new InterpretationResult(false, new Unactionable($"Could not interpret {input}"));
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            var l = new List<CommandHelp>();

            foreach (var interpreter in Interpreters)
            {
                var contextualCommands = interpreter.GetContextualCommandHelp(game);

                if (contextualCommands != null)
                    l.AddRange(interpreter.GetContextualCommandHelp(game));
            }

            return l.ToArray();
        }

        #endregion
    }
}
