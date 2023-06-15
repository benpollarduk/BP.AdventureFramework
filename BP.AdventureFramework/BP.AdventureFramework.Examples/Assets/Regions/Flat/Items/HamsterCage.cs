using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class HamsterCage : ItemTemplate<HamsterCage>
    {
        #region Constants

        internal const string Name = "Hamster Cage";
        private const string Description = "There is a pretty large hamster cage on the floor. When you go up to it you hear a small, but irritated sniffing. Mable sounds annoyed, best leave her alone for now.";

        #endregion

        #region Overrides of ItemTemplate<ConchShell>

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
