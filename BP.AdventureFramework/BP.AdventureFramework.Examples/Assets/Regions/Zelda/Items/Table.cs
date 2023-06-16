using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Table : ItemTemplate<Table>
    {
        #region Constants

        internal const string Name = "Table";
        private const string Description = "A small wooden table made from a slice of a trunk of a Deku tree. Pretty handy, but you can't take it with you.";

        #endregion

        #region Overrides of ItemTemplate<Table>

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
