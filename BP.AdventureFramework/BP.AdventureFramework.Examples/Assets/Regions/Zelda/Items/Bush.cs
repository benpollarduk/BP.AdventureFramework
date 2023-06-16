using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items
{
    public class Bush : ItemTemplate<Bush>
    {
        #region Constants

        internal const string Name = "Bush";
        private const string Description = "The bush is small, but very dense. Something is gleaming inside, but you cant reach it because the bush is so thick.";

        #endregion

        #region Overrides of ItemTemplate<Bush>

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
