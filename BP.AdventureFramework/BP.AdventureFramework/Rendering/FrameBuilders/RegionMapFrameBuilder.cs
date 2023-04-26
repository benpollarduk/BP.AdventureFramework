using System;
using System.Text;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    public class RegionMapFrameBuilder : IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the frame drawer.
        /// </summary>
        public FrameDrawer FrameDrawer { get; }

        /// <summary>
        /// Get the map drawer.
        /// </summary>
        public MapDrawer MapDrawer { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RegionMapFrameBuilder class.
        /// </summary>
        /// <param name="frameDrawer">A drawer to use for the frame.</param>
        /// <param name="mapDrawer">A drawer to use for the map.</param>
        public RegionMapFrameBuilder(FrameDrawer frameDrawer, MapDrawer mapDrawer)
        {
            FrameDrawer = frameDrawer;
            MapDrawer = mapDrawer;
        }

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public Frame Build(Region region, int width, int height)
        {
            var builder = new StringBuilder();

            builder.Append(FrameDrawer.ConstructDivider(width));
            builder.Append(FrameDrawer.ConstructWrappedPaddedString(region.Identifier.Name, width, true));
            builder.Append(FrameDrawer.ConstructDivider(width));

            if (MapDrawer != null)
            {
                var map = MapDrawer.ConstructRegionMap(region, width, height - (FrameDrawer.DetermineLinesInString(builder.ToString()) + 5));
                builder.Append(FrameDrawer.ConstructPaddedArea(width, (height - FrameDrawer.DetermineLinesInString(builder.ToString()) - FrameDrawer.DetermineLinesInString(map)) / 2));
                builder.Append(map);
                builder.Append(FrameDrawer.ConstructPaddedArea(width, height - FrameDrawer.DetermineLinesInString(builder.ToString()) - 2));
            }

            builder.Append(FrameDrawer.ConstructDivider(width).Replace(Environment.NewLine, ""));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
