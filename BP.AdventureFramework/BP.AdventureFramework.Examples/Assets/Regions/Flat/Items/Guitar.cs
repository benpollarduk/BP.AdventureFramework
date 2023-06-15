using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Guitar : ItemTemplate<Guitar>
    {
        #region Constants

        internal const string Name = "Guitar";
        private const string Description = "The guitar is blue, with birds inlaid on the fret board. On the headstock is someones name... 'Paul Reed Smith'. Who the hell is that. The guitar is literally begging to be played...";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected override Item OnCreate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
