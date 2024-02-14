using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda
{
    internal class Zelda : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Kokiri Forest";
        private const string Description = "The home of the Kokiri tree folk.";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new LinksHouse().Instantiate(),
                [0, 1, 0] = new OutsideLinksHouse().Instantiate(),
                [1, 1, 0] = new TailCave().Instantiate(),
                [0, 2, 0] = new Stream().Instantiate()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
