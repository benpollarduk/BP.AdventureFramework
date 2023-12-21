﻿using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades
{
    internal class Everglades : RegionTemplate<Everglades>
    {
        #region Constants

        private const string Name = "Everglades";
        private const string Description = "The starting place.";

        #endregion

        #region Overrides of RegionTemplate<Everglades>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [2, 0, 0] = ForestEntrance.Create(),
                [2, 1, 0] = ForestFloor.Create(),
                [2, 2, 0] = CaveMouth.Create(),
                [1, 2, 0] = GreatWesternOcean.Create(),
                [2, 3, 0] = Cave.Create(),
                [3, 3, 0] = InnerCave.Create(),
                [3, 4, 0] = Outskirts.Create()
            };


            return regionMaker.Make(2, 0, 0);
        }

        #endregion
    }
}
