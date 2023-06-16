using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class LoungeTV : ItemTemplate<LoungeTV>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "The TV is large, and is playing some program with a Chinese looking man dressing a half naked middle aged woman.";

        #endregion

        #region Overrides of ItemTemplate<LoungeTV>

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
