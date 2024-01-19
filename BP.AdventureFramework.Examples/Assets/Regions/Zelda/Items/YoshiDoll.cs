using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class YoshiDoll : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Yoshi Doll";
        private const string Description = "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint...";

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
