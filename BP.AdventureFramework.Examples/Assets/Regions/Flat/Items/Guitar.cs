using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Guitar : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Guitar";
        private const string Description = "The guitar is blue, with birds inlaid on the fret board. On the headstock is someones name... 'Paul Reed Smith'. Who the hell is that. The guitar is literally begging to be played...";

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
