using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class HamsterCage : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Hamster Cage";
        private const string Description = "There is a pretty large hamster cage on the floor. When you go up to it you hear a small, but irritated sniffing. Mable sounds annoyed, best leave her alone for now.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new Item(Name, Description);
        }

        #endregion
    }
}
