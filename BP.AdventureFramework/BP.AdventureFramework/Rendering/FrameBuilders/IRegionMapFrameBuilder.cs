using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any objects that can build region map frames.
    /// </summary>
    public interface IRegionMapFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="mapDrawer">The map drawer.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        Frame Build(Region region, MapDrawer mapDrawer, FrameDrawer frameDrawer, int width, int height);
    }
}
