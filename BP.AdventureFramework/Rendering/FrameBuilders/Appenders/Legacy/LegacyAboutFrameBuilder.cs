using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy about frames.
    /// </summary>
    public sealed class LegacyAboutFrameBuilder : IAboutFrameBuilder
    {
        #region Fields

        private LineStringBuilder lineStringBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyAboutFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        public LegacyAboutFrameBuilder(LineStringBuilder lineStringBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
        }

        #endregion

        #region Implementation of IAboutFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(string title, Game game, int width, int height)
        {
            var divider = lineStringBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(title, width, true);
            constructedScene += divider;
            constructedScene += lineStringBuilder.BuildWrappedPadded(game.Name, width, true);
            constructedScene += lineStringBuilder.BuildPaddedArea(width, 1);
            constructedScene += lineStringBuilder.BuildWrappedPadded(game.Description, width, true);
            constructedScene += lineStringBuilder.BuildPaddedArea(width, 5);

            if (!string.IsNullOrEmpty(game.Author))
                constructedScene += lineStringBuilder.BuildWrappedPadded($"Created by: {game.Author}.", width, true);

            constructedScene += lineStringBuilder.BuildWrappedPadded("BP.AdventureFramework by Ben Pollard 2011 - 2023", width, true);
            constructedScene += lineStringBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 1);
            constructedScene += divider.Replace(lineStringBuilder.LineTerminator, string.Empty);

            return new TextFrame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
