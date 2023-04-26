namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any objects that can build end frames.
    /// </summary>
    public interface IEndFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        Frame Build(string message, string reason, int width, int height);
    }
}
