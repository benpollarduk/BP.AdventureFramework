using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Hub.Rooms;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Hub
{
    internal class Hub : RegionTemplate<Hub>
    {
        #region Constants

        private const string Name = "Jungle";
        private const string Description = "A dense jungle, somewhere tropical.";

        #endregion

        #region Overrides of RegionTemplate<Hub>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = Clearing.Create()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
