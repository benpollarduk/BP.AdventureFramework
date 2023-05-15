using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build region map frames.
    /// </summary>
    public interface IRegionMapFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        IFrame Build(Region region, int width, int height);
    }
}
