using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Toilet : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Toilet";
        private const string Description = "A clean looking toilet. You lift the lid to take a look inside... ergh a floater! You flush the toilet but it just churns around! You close the lid and pretend it isn't there.";

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
