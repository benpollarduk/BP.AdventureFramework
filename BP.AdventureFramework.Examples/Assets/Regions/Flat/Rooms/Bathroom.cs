using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Bathroom : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Bathroom";
        private const string Description = "The bathroom is fairly small. There are some clothes drying on a clothes horse. A bath lies along the eastern wall. There is a remarkably clean toilet and sink along the western wall, with a mirror above the sink. To the north is a large window, it is open and you can see out onto the roof of the flat below. The doorway to the south leads into the Western Hallway.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South));

            room.AddItem(new Bath().Instantiate());
            room.AddItem(new Toilet().Instantiate());
            room.AddItem(new Mirror().Instantiate());

            return room;
        }

        #endregion
    }
}
