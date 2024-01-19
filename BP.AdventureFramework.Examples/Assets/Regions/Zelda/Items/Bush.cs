using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Bush : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Bush";
        private const string Description = "The bush is small, but very dense. Something is gleaming inside, but you cant reach it because the bush is so thick.";

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
