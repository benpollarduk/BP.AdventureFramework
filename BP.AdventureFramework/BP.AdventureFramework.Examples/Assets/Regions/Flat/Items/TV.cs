using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class TV : ItemTemplate<TV>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "The TV is small - the screen is only 14\"! Two DVD's are propped alongside it 'Miranda' and 'The Vicar Of Dibly'.";

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
