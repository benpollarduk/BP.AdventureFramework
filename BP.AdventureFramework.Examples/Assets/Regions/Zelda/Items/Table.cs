using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Table : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Table";
        private const string Description = "A small wooden table made from a slice of a trunk of a Deku tree. Pretty handy, but you can't take it with you.";

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
