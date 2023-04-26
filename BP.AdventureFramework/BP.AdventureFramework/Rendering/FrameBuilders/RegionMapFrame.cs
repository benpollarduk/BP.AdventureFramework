using System.Text;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    public class RegionMapFrame : IRegionMapFrameBuilder
    {
        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="mapDrawer">The map drawer.</param>
        /// <param name="frameDrawer">Specify the frame drawer.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(Region region, MapDrawer mapDrawer, FrameDrawer frameDrawer, int width, int height)
        {
            var builder = new StringBuilder();

            builder.Append(frameDrawer.ConstructDivider(width));
            builder.Append(frameDrawer.ConstructWrappedPaddedString(region.Identifier.Name, width, true));
            builder.Append(frameDrawer.ConstructDivider(width));

            if (mapDrawer != null)
            {
                var map = mapDrawer.ConstructRegionMap(region, width, height - (frameDrawer.DetermineLinesInString(builder.ToString()) + 5));
                builder.Append(frameDrawer.ConstructPaddedArea(width, (height - frameDrawer.DetermineLinesInString(builder.ToString()) - frameDrawer.DetermineLinesInString(map)) / 2));
                builder.Append(map);
                builder.Append(frameDrawer.ConstructPaddedArea(width, height - frameDrawer.DetermineLinesInString(builder.ToString()) - 2));
            }

            builder.Append(frameDrawer.ConstructDivider(width).Replace("\n", ""));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
