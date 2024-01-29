using System.Linq;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Conversations
{
    /// <summary>
    /// Represents a conversation.
    /// </summary>
    public sealed class Conversation
    {
        #region Fields

        private Response selectedResponse;

        #endregion

        #region Properties

        /// <summary>
        /// Get the current paragraph in the conversation.
        /// </summary>
        public Paragraph CurrentParagraph { get; private set; }

        /// <summary>
        /// Get the current paragraph in the conversation.
        /// </summary>
        public Paragraph[] Paragraphs { get; }

        /// <summary>
        /// Get the log.
        /// </summary>
        public LogItem[] Log { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        /// <param name="paragraphs">The paragraphs.</param>
        public Conversation(params Paragraph[] paragraphs)
        {
            Paragraphs = paragraphs;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shift the conversation to a new paragraph.
        /// </summary>
        /// <param name="index">The index of the paragraph to shift to.</param>
        /// <returns>The new paragraph.</returns>
        private Paragraph Shift(int index)
        {
            return index >= 0 && index < Paragraphs.Length ? Paragraphs[index] : CurrentParagraph;
        }

        /// <summary>
        /// Trigger the next line in this conversation.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The reaction to the line.</returns>
        public Reaction Next(Game game)
        {
            if (Paragraphs == null || !Paragraphs.Any())
                return new Reaction(ReactionResult.Internal, "No paragraphs.");

            var entryParagraph = CurrentParagraph;

            if (CurrentParagraph?.CanRespond ?? false)
            {
                if (selectedResponse != null)
                {
                    CurrentParagraph = Shift(selectedResponse.Instruction.GetIndexOfNext(CurrentParagraph, Paragraphs));
                    selectedResponse = null;
                }
                else
                {
                    return new Reaction(ReactionResult.Internal, "Awaiting response.");
                }
            }
            else if (CurrentParagraph == null)
            {
                CurrentParagraph = Paragraphs[0];
            }
            else
            {
                CurrentParagraph = Shift(CurrentParagraph.Instruction.GetIndexOfNext(CurrentParagraph, Paragraphs));
            }

            if ((CurrentParagraph == null) || (CurrentParagraph == entryParagraph))
                return new Reaction(ReactionResult.Internal, "End of conversation.");

            CurrentParagraph.Action?.Invoke(game);

            var line = CurrentParagraph.Line.ToSpeech();
            Log = Log.Add(new LogItem(Participant.Other, line));

            return new Reaction(ReactionResult.Internal, line);
        }

        /// <summary>
        /// Respond to the conversation.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="game">The game.</param>
        /// <returns>The reaction to the response.</returns>
        public Reaction Respond(Response response, Game game)
        {
            if (response == null)
                return new Reaction(ReactionResult.Error, "No response.");

            if (CurrentParagraph == null)
                return new Reaction(ReactionResult.Error, "No paragraph.");

            if (!CurrentParagraph.Responses?.Contains(response) ?? true)
                return new Reaction(ReactionResult.Error, "Invalid response.");

            Log = Log.Add(new LogItem(Participant.Player, response.Line.EnsureFinishedSentence().ToSpeech()));

            selectedResponse = response;

            return Next(game);
        }

        #endregion
    }
}
