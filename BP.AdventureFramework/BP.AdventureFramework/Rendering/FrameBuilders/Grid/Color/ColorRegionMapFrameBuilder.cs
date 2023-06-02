using System;
using System.Drawing;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color
{
    /// <summary>
    /// Provides a builder of color region map frames.
    /// </summary>
    public sealed class ColorRegionMapFrameBuilder : IRegionMapFrameBuilder
    {
        #region Fields

        private readonly GridStringBuilder gridStringBuilder;

        #endregion

        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; }

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public RenderColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public RenderColor BorderColor { get; set; } = RenderColor.DarkGray;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public RenderColor TitleColor { get; set; } = RenderColor.White;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorRegionMapFrameBuilder class.
        /// </summary>
        /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
        /// <param name="regionMapBuilder">A builder for region maps.</param>
        public ColorRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, IRegionMapBuilder regionMapBuilder)
        {
            this.gridStringBuilder = gridStringBuilder;
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
        public IFrame Build(Region region, int width, int height)
        {
            gridStringBuilder.Resize(new Size(width, height));

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(region.Identifier.Name, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, region.Identifier.Name.Length, TitleColor);

            RegionMapBuilder?.BuildRegionMap(gridStringBuilder, region, 2, lastY + 2, availableWidth, height - 4);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { AcceptsInput = false, ShowCursor = false };
        }

        #endregion
    }
}
