using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades
{
    internal class Everglades : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Everglades";
        private const string Description = "The starting place.";

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
                [2, 0, 0] = new ForestEntrance().Instantiate(),
                [2, 1, 0] = new ForestFloor().Instantiate(),
                [2, 2, 0] = new CaveMouth().Instantiate(),
                [1, 2, 0] = new GreatWesternOcean().Instantiate(),
                [2, 3, 0] = new Cave().Instantiate(),
                [3, 3, 0] = new InnerCave().Instantiate(),
                [3, 4, 0] = new Outskirts().Instantiate()
            };


            return regionMaker.Make(2, 0, 0);
        }

        #endregion
    }
}
