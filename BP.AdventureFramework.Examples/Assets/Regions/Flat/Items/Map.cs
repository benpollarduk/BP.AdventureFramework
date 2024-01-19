using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Map : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Map";
        private const string Description = "This things huge! Who would buy one of these? It looks pretty cheap, like it could have been bought from one of those massive Swedish outlets. The resolution of the map is too small to see your road on.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
