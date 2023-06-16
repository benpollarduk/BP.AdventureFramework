using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Bath : ItemTemplate<Bath>
    {
        #region Constants

        internal const string Name = "Bath";
        private const string Description = "A long but narrow bath. You want to fill it but you can't because there is a wetsuit in it.";

        #endregion

        #region Overrides of ItemTemplate<Bath>

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
