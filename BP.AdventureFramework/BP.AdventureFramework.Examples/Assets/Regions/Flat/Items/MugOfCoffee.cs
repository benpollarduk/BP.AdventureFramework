using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class MugOfCoffee : ItemTemplate<MugOfCoffee>
    {
        #region Constants

        internal const string Name = "Mug Of Coffee";
        private const string Description = "Hmmm smells good, nice and bitter!";

        #endregion

        #region Overrides of ItemTemplate<MugOfCoffee>

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
