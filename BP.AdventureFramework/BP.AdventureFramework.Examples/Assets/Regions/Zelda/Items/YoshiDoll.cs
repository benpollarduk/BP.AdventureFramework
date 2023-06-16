using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class YoshiDoll : ItemTemplate<YoshiDoll>
    {
        #region Constants

        internal const string Name = "Yoshi Doll";
        private const string Description = "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint...";

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
