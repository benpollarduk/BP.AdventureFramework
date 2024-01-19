using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Items
{
    public class Knife : ItemTemplate
    {
        #region Constants

        internal const string Name = "Knife";
        private const string Description = "A small pocket knife.";

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
