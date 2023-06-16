using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Skylight : ItemTemplate<Skylight>
    {
        #region Constants

        internal const string Name = "Skylight";
        private const string Description = "You peer down into the skylight, only to see a naked Italian man... cooking! Yikes! Not liking the idea of the accidents one could get into by cooking naked you look away quickly.";

        #endregion

        #region Overrides of ItemTemplate<Skylight>

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
