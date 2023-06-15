using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Items
{
    public class Knife : ItemTemplate<Knife>
    {
        #region Constants

        internal const string Name = "Knife";
        private const string Description = "A small pocket knife.";

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
