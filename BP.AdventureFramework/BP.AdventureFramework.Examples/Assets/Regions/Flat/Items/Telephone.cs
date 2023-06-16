using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Telephone : ItemTemplate<Telephone>
    {
        #region Constants

        internal const string Name = "TV";
        private const string Description = "As soon as you pickup the telephone to examine it you hear hideous feedback. You replace it quickly!";

        #endregion

        #region Overrides of ItemTemplate<Telephone>

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
