using System;
using System.Text;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Drawers;
using BP.AdventureFramework.Rendering.MapBuilders;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy region map frames.
    /// </summary>
    public class LegacyRegionMapFrameBuilder : IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the drawer.
        /// </summary>
        public Drawer Drawer { get; }

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        public IRegionMapBuilder RegionMapBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyRegionMapFrameBuilder class.
        /// </summary>
        /// <param name="drawer">A drawer to use for the frame.</param>
        /// <param name="regionMapBuilder">A builder for region maps.</param>
        public LegacyRegionMapFrameBuilder(Drawer drawer, IRegionMapBuilder regionMapBuilder)
        {
            Drawer = drawer;
            RegionMapBuilder = regionMapBuilder;
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

            builder.Append(Drawer.ConstructDivider(width));
            builder.Append(Drawer.ConstructWrappedPaddedString(region.Identifier.Name, width, true));
            builder.Append(Drawer.ConstructDivider(width));

            if (RegionMapBuilder != null)
            {
                var map = RegionMapBuilder.BuildRegionMap(region, width, height - (builder.ToString().LineCount() + 5));
                builder.Append(Drawer.ConstructPaddedArea(width, (height - builder.ToString().LineCount() - map.LineCount()) / 2));
                builder.Append(map);
                builder.Append(Drawer.ConstructPaddedArea(width, height - builder.ToString().LineCount() - 2));
            }

            builder.Append(Drawer.ConstructDivider(width).Replace(Environment.NewLine, ""));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
