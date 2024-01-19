using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Hub.Rooms;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Hub
{
    internal class Hub : RegionTemplate
    {
        #region Constants

        private const string Name = "Jungle";
        private const string Description = "A dense jungle, somewhere tropical.";

        #endregion

        #region Overrides of RegionTemplate

        /// <summary>
        /// Instantiate a new instance of the region.
        /// </summary>
        /// <returns>The region.</returns>
        public override Region Instantiate()
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = new Clearing().Instantiate()
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
