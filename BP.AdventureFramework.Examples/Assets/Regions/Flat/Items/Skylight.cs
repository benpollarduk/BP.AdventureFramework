using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Skylight : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Skylight";
        private const string Description = "You peer down into the skylight, only to see a naked Italian man... cooking! Yikes! Not liking the idea of the accidents one could get into by cooking naked you look away quickly.";

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
