using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Items
{
    public class Canvas : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Canvas";
        private const string Description = "Wow, cool canvas. It is brightly painted with aliens and planets. On one planet there is a rabbit playing a guitar and whistling, but you can't see his face because he has his back turned to you. Something looks wrong with the rabbit...";

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
