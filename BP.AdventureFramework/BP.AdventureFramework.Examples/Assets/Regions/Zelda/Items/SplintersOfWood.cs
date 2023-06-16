using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class SplintersOfWood : ItemTemplate<SplintersOfWood>
    {
        #region Constants

        internal const string Name = "Splinters Of Wood";
        private const string Description = "Some splinters of wood left from your chopping frenzy on the stump.";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

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
