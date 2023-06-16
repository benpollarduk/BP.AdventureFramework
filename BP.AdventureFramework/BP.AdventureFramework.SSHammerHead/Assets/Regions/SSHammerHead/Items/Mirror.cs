using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class Mirror : ItemTemplate<Mirror>
    {
        #region Constants

        internal const string Name = "Mirror";
        private const string Description = "A thin telescopic pole with small mirror on the end, enables you to see in tight spaces.";

        #endregion

        #region Overrides of ItemTemplate<Mirror>

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
