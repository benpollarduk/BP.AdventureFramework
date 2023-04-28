using BP.AdventureFramework.Logic;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any objects that can build about frames.
    /// </summary>
    public interface IAboutFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="game">The game.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        Frame Build(string title, Game game, int width, int height);
    }
}
