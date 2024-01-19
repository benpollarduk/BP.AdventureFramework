using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Shield : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Shield";
        private const string Description = "A small wooden shield. It has the Deku mark painted on it in red, the sign of the forest.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
