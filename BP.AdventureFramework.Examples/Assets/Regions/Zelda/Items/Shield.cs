using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Shield : ItemTemplate
    {
        #region Constants

        internal const string Name = "Shield";
        private const string Description = "A small wooden shield. It has the Deku mark painted on it in red, the sign of the forest.";

        #endregion

        #region Overrides of ItemTemplate

        /// <summary>
        /// Instantiate a new instance of the item.
        /// </summary>
        /// <returns>The item.</returns>
        public override Item Instantiate()
        {
            return new Item(Name, Description, true);
        }

        #endregion
    }
}
