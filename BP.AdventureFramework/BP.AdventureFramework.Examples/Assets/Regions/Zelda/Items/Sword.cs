using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Sword : ItemTemplate<Sword>
    {
        #region Constants

        internal const string Name = "Sword";
        private const string Description = "A small sword handed down by the Kokiri. It has a wooden handle but the blade is sharp.";

        #endregion

        #region Overrides of ItemTemplate<Sword>

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
