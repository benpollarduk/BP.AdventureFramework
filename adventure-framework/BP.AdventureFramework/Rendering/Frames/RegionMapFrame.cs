using System.Text;
using AdventureFramework.Locations;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for displaying a Region map
    /// </summary>
    public class RegionMapFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set the Region
        /// </summary>
        public Region Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// Get or set the Region
        /// </summary>
        private Region region;

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        public MapDrawer MapDrawer
        {
            get { return mapDrawer; }
            set { mapDrawer = value; }
        }

        /// <summary>
        /// Get or set the drawer used for constructing room maps
        /// </summary>
        private MapDrawer mapDrawer = new MapDrawer();

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the RegionMapFrame class
        /// </summary>
        public RegionMapFrame()
        {
            // no cursor
            ShowCursor = false;

            // no accept of input
            AcceptsInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the RegionMapFrame class
        /// </summary>
        /// <param name="region">Specify the Region</param>
        /// <param name="mapDrawer">Sepcify a drawer for constructing room maps</param>
        public RegionMapFrame(Region region, MapDrawer mapDrawer)
        {
            // set region
            Region = region;

            // set drawer
            MapDrawer = mapDrawer;

            // no cursor
            ShowCursor = false;

            // no accept of input
            AcceptsInput = false;
        }

        /// <summary>
        /// Build this RegionMapFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // hold scene
            var scene = new StringBuilder();

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // add title
            scene.Append(drawer.ConstructWrappedPaddedString(Region.Name, width, true));

            // add devider
            scene.Append(drawer.ConstructDevider(width));

            // if a map drawer
            if (MapDrawer != null)
            {
                // get map
                var map = MapDrawer.ConstructRegionMap(Region, width, height - (drawer.DetermineLinesInString(scene.ToString()) + 5));

                // add map spacer
                scene.Append(drawer.ConstructPaddedArea(width, (height - drawer.DetermineLinesInString(scene.ToString()) - drawer.DetermineLinesInString(map)) / 2));

                // add map
                scene.Append(map);

                // add bottom spacer
                scene.Append(drawer.ConstructPaddedArea(width, height - drawer.DetermineLinesInString(scene.ToString()) - 2));
            }

            // add devider
            scene.Append(drawer.ConstructDevider(width).Replace("\n", ""));

            // return the scene
            return scene.ToString();
        }

        #endregion
    }
}