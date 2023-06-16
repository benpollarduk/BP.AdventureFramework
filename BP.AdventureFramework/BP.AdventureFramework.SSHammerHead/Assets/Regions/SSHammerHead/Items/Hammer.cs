using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Hammer : ItemTemplate<Hammer>
    {
        #region Constants

        internal const string Name = "Hammer";
        private const string Description = "A small utility hammer use for small engineering tasks.";

        #endregion

        #region Overrides of ItemTemplate<Hammer>

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
