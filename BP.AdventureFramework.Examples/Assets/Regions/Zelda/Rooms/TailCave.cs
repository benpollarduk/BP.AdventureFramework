using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class TailCave : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Tail Cave";
        private const string Description = "The cave is dark, and currently very empty. Quite shabby really, not like the cave on Koholint at all...";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
