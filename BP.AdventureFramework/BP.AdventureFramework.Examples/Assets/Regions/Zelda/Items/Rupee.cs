using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Rupee : ItemTemplate<Rupee>
    {
        #region Constants

        internal const string Name = "Rupee";
        private const string Description = "A red rupee! Wow this thing is worth 10 green rupees.";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description, true) { IsPlayerVisible = false };
        }

        #endregion
    }
}
