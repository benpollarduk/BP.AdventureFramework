using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class ForestFloor : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Forest Floor";
        private const string Description = "The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South));
        }

        #endregion
    }
}
