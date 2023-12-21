using System.Collections.Generic;
using BP.AdventureFramework.Commands.Conversation;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting conversation commands.
    /// </summary>
    internal class ConversationCommandInterpreter : IInterpreter
    {
        #region Constants

        /// <summary>
        /// Get the end command.
        /// </summary>
        public const string End = "End";

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        {
            new CommandHelp(End, "End the conversation")
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
            if (game.ActiveConverser == null)
                return InterpretationResult.Fail;

            if (input.InsensitiveEquals(End))
                return new InterpretationResult(true, new End());

            if (string.IsNullOrEmpty(input.Trim()))
                return new InterpretationResult(true, new Next());

            if (!int.TryParse(input, out var index))
                return InterpretationResult.Fail;
            
            if (index > 0 && index <= game.ActiveConverser.Conversation?.CurrentParagraph?.Responses?.Length)
                return new InterpretationResult(true, new Respond(game.ActiveConverser.Conversation.CurrentParagraph.Responses[index - 1]));

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            if (game.ActiveConverser?.Conversation == null) 
                return new CommandHelp[0];

            var commands = new List<CommandHelp> { new CommandHelp(End, "End the conversation") };

            if (game.ActiveConverser.Conversation.CurrentParagraph?.CanRespond ?? false)
            {
                for (var i = 0; i < game.ActiveConverser.Conversation.CurrentParagraph.Responses.Length; i++)
                {
                    var response = game.ActiveConverser.Conversation.CurrentParagraph.Responses[i];
                    commands.Add(new CommandHelp((i + 1).ToString(), response.Line.EnsureFinishedSentence().ToSpeech()));
                }
            }

            return commands.ToArray();
        }

        #endregion
    }
}
