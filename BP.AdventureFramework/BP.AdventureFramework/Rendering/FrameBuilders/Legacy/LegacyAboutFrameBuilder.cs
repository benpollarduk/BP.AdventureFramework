using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Rendering.LayoutBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy about frames.
    /// </summary>
    public sealed class LegacyAboutFrameBuilder : IAboutFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the string layout builder.
        /// </summary>
        private IStringLayoutBuilder StringLayoutBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyAboutFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        public LegacyAboutFrameBuilder(IStringLayoutBuilder stringLayoutBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
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
        public Frame Build(string title, Game game, int width, int height)
        {
            var divider = StringLayoutBuilder.BuildHorizontalDivider(width);
            var constructedScene = divider;
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(title, width, true);
            constructedScene += divider;
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(game.Name, width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, 1);
            constructedScene += StringLayoutBuilder.BuildWrappedPadded(game.Description, width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, 5);

            if (!string.IsNullOrEmpty(game.Author))
                constructedScene += StringLayoutBuilder.BuildWrappedPadded($"Created by: {game.Author}.", width, true);

            constructedScene += StringLayoutBuilder.BuildWrappedPadded("BP.AdventureFramework by Ben Pollard 2011 - 2023.", width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height / 2 - constructedScene.LineCount());
            constructedScene += StringLayoutBuilder.BuildWrappedPadded("Press Enter to start", width, true);
            constructedScene += StringLayoutBuilder.BuildPaddedArea(width, height - constructedScene.LineCount() - 1);
            constructedScene += divider.Replace(StringLayoutBuilder.LineTerminator, string.Empty);

            return new Frame(constructedScene, 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
