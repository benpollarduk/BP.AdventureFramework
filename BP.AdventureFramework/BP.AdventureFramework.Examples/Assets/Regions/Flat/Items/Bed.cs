using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Bed : ItemTemplate<Bed>
    {
        #region Constants

        internal const string Name = "Bed";
        private const string Description = "The bed is neatly made, Beth makes it every day. By your reckoning there are way too many cushions on it though...";

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
