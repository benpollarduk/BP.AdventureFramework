using System.Text;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy
{
    /// <summary>
    /// Provides a builder of legacy region map frames.
    /// </summary>
    public sealed class LegacyRegionMapFrameBuilder : IRegionMapFrameBuilder
    {
        #region Fields

        private readonly LineStringBuilder lineStringBuilder;
        private readonly IRegionMapBuilder regionMapBuilder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LegacyRegionMapFrameBuilder class.
        /// </summary>
        /// <param name="lineStringBuilder">A builder to use for the string layout.</param>
        /// <param name="regionMapBuilder">A builder for region maps.</param>
        public LegacyRegionMapFrameBuilder(LineStringBuilder lineStringBuilder, IRegionMapBuilder regionMapBuilder)
        {
            this.lineStringBuilder = lineStringBuilder;
            this.regionMapBuilder = regionMapBuilder;
        }

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        public IFrame Build(Region region, int width, int height)
        {
            var builder = new StringBuilder();

            builder.Append(lineStringBuilder.BuildHorizontalDivider(width));
            builder.Append(lineStringBuilder.BuildWrappedPadded(region.Identifier.Name, width, true));
            builder.Append(lineStringBuilder.BuildHorizontalDivider(width));

            if (regionMapBuilder != null)
            {
                var map = regionMapBuilder.BuildRegionMap(lineStringBuilder,region, width, height - (builder.ToString().LineCount() + 5));
                builder.Append(lineStringBuilder.BuildPaddedArea(width, (height - builder.ToString().LineCount() - map.LineCount()) / 2));
                builder.Append(map);
                builder.Append(lineStringBuilder.BuildPaddedArea(width, height - builder.ToString().LineCount() - 1));
            }

            builder.Append(lineStringBuilder.BuildHorizontalDivider(width).Replace(lineStringBuilder.LineTerminator, string.Empty));

            return new TextFrame(builder.ToString(), 0, 0) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
