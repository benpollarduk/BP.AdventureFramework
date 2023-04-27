using System;
using System.Text;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Drawers;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy region map frames.
    /// </summary>
    public class LegacyRegionMapFrameBuilder : IRegionMapFrameBuilder
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
        /// Initializes a new instance of the LegacyRegionMapFrameBuilder class.
        /// </summary>
        /// <param name="frameDrawer">A drawer to use for the frame.</param>
        /// <param name="mapDrawer">A drawer to use for the map.</param>
        public LegacyRegionMapFrameBuilder(FrameDrawer frameDrawer, MapDrawer mapDrawer)
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
                var map = MapDrawer.ConstructRegionMap(region, width, height - (builder.ToString().LineCount() + 5));
                builder.Append(FrameDrawer.ConstructPaddedArea(width, (height - builder.ToString().LineCount() - map.LineCount()) / 2));
                builder.Append(map);
                builder.Append(FrameDrawer.ConstructPaddedArea(width, height - builder.ToString().LineCount() - 2));
            }

            builder.Append(FrameDrawer.ConstructDivider(width).Replace(Environment.NewLine, ""));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
