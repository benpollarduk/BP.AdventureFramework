using System.Collections.Generic;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any objects that can build help frames.
    /// </summary>
    public interface IHelpFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commands">The command dictionary.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        Frame Build(string title, string description, Dictionary<string, string> commands, FrameDrawer frameDrawer, int width, int height);
    }
}
