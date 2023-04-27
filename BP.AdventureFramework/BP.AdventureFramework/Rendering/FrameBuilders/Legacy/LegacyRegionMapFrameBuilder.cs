using System;
using System.Text;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.LayoutBuilders;
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
        /// Get the string layout builder.
        /// </summary>
        public IStringLayoutBuilder StringLayoutBuilder { get; }

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        public IRegionMapBuilder RegionMapBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyRegionMapFrameBuilder class.
        /// </summary>
        /// <param name="stringLayoutBuilder">A builder to use for the string layout.</param>
        /// <param name="regionMapBuilder">A builder for region maps.</param>
        public LegacyRegionMapFrameBuilder(IStringLayoutBuilder stringLayoutBuilder, IRegionMapBuilder regionMapBuilder)
        {
            StringLayoutBuilder = stringLayoutBuilder;
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

            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width));
            builder.Append(StringLayoutBuilder.BuildWrappedPadded(region.Identifier.Name, width, true));
            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width));

            if (RegionMapBuilder != null)
            {
                var map = RegionMapBuilder.BuildRegionMap(region, width, height - (builder.ToString().LineCount() + 5));
                builder.Append(StringLayoutBuilder.BuildPaddedArea(width, (height - builder.ToString().LineCount() - map.LineCount()) / 2));
                builder.Append(map);
                builder.Append(StringLayoutBuilder.BuildPaddedArea(width, height - builder.ToString().LineCount() - 2));
            }

            builder.Append(StringLayoutBuilder.BuildHorizontalDivider(width).Replace(Environment.NewLine, ""));

            return new Frame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
