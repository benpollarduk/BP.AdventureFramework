using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class EasternHallway : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Eastern Hallway";
        private const string Description = "The hallway is pretty narrow, and all the walls are bare except for a strange looking telephone. To the east is the front door, but it looks to heavy to open. To the south is the bedroom, to the west the hallway continues.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.East, true), new Exit(Direction.West));

            room.AddItem(new Telephone().Instantiate());

            return room;
        }

        #endregion
    }
}
