using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy conversation frames.
    /// </summary>
    public sealed class LegacyConversationFrameBuilder : IConversationFrameBuilder
    {
        #region Fields

        private readonly LineStringBuilder lineStringBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyConversationFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        public LegacyConversationFrameBuilder(LineStringBuilder lineStringBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
        }

        #endregion

        #region Implementation of IConversationFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="converser">The converser.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, int width, int height)
        {
            var whitespace = lineStringBuilder.BuildWrappedPadded(string.Empty, width, false);
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(title, width, true);
            constructedScene += divider;
            constructedScene += whitespace;

            if (converser != null)
            {
                constructedScene += lineStringBuilder.BuildWrappedPadded("CONVERSATION:", width, false);
                constructedScene += whitespace;
                constructedScene += lineStringBuilder.BuildWrappedPadded($"{converser.Identifier.Name}: {converser.Conversation?.Log?.LastOrDefault()?.Line?.ToSpeech() ?? ""}", width, false);
                constructedScene += whitespace;
                constructedScene += divider;
                constructedScene += whitespace;
            }
            
            if (contextualCommands?.Any() ?? false)
            {
                constructedScene += lineStringBuilder.BuildWrappedPadded("COMMANDS:", width, false);
                constructedScene += whitespace;

                foreach (var contextualCommand in contextualCommands)
                    constructedScene += lineStringBuilder.BuildWrappedPadded($"{contextualCommand.Command}: {contextualCommand.Description}", width, false);

                constructedScene += whitespace;
            }

            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 5);

            constructedScene += divider;
            var yPositionOfCursor = constructedScene.LineCount();
            constructedScene += lineStringBuilder.BuildWrappedPadded("WHAT DO YOU DO? ", width, false);
            constructedScene += divider;
            constructedScene += whitespace;
            constructedScene += divider.Replace(lineStringBuilder.LineTerminator, string.Empty);

            return new TextFrame(constructedScene, 18, yPositionOfCursor) { AcceptsInput = true, ShowCursor = true };
        }

        #endregion
    }
}
