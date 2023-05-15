using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build help frames.
    /// </summary>
    public interface IHelpFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        IFrame Build(string title, string description, CommandHelp[] commandHelp, int width, int height);
    }
}
