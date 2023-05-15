using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build game over frames.
    /// </summary>
    public interface IGameOverFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        IFrame Build(string message, string reason, int width, int height);
    }
}
