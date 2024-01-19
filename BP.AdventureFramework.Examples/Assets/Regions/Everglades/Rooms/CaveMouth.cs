using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class CaveMouth : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Cave Mouth";
        private const string Description = "A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.West));
        }

        #endregion
    }
}
