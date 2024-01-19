using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class Cave : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Cave";
        private const string Description = "The cave is so dark you struggling to see. A screeching noise is audible to the east.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.East), new Exit(Direction.South));
        }

        #endregion
    }
}
