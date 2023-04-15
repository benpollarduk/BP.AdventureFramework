using System.Text;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying a Region map.
    /// </summary>
    internal sealed class RegionMapFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the Region.
        /// </summary>
        public Region Region { get; set; }

        /// <summary>
        /// Get or set the drawer used for constructing room maps.
        /// </summary>
        public MapDrawer MapDrawer { get; set; } = new MapDrawer();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RegionMapFrame class.
        /// </summary>
        public RegionMapFrame()
        {
            ShowCursor = false;
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the RegionMapFrame class.
        /// </summary>
        /// <param name="region">Specify the Region.</param>
        /// <param name="mapDrawer">Specify a drawer for constructing room maps.</param>
        public RegionMapFrame(Region region, MapDrawer mapDrawer)
        {
            Region = region;
            MapDrawer = mapDrawer;
            ShowCursor = false;
            AcceptsInput = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Build this RegionMapFrame into a text based display.
        /// </summary>
        /// <param name="width">Specify the width of the Frame.</param>
        /// <param name="height">Specify the height of the Frame.</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with.</param>
        /// <returns>A string representing the Frame.</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            var scene = new StringBuilder();

            scene.Append(drawer.ConstructDivider(width));
            scene.Append(drawer.ConstructWrappedPaddedString(Region.Identifier.Name, width, true));
            scene.Append(drawer.ConstructDivider(width));

            if (MapDrawer != null)
            {
                var map = MapDrawer.ConstructRegionMap(Region, width, height - (drawer.DetermineLinesInString(scene.ToString()) + 5));
                scene.Append(drawer.ConstructPaddedArea(width, (height - drawer.DetermineLinesInString(scene.ToString()) - drawer.DetermineLinesInString(map)) / 2));
                scene.Append(map);
                scene.Append(drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(scene.ToString()) - 2));
            }

            scene.Append(drawer.ConstructDivider(width).Replace("\n", ""));

            return scene.ToString();
        }

        #endregion
    }
}