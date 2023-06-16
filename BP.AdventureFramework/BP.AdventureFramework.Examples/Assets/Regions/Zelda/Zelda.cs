using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda
{
    internal class Zelda : RegionTemplate<Zelda>
    {
        #region Constants

        private const string Name = "Kokiri Forest";
        private const string Description = "The home of the Kokiri tree folk.";

        #endregion

        #region Overrides of RegionTemplate<Zelda>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = LinksHouse.Create(),
                [0, 1, 0] = OutsideLinksHouse.Create(pC),
                [1, 1, 0] = TailCave.Create(),
                [0, 2, 0] = Stream.Create()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
