using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Map : ItemTemplate<Map>
    {
        #region Constants

        internal const string Name = "Map";
        private const string Description = "This things huge! Who would buy one of these? It looks pretty cheap, like it could have been bought from one of those massive Swedish outlets. The resolution of the map is too small to see your road on.";

        #endregion

        #region Overrides of ItemTemplate<Map>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
