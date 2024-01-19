using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class LinksHouse : RoomTemplate
    {
        #region Constants

        private const string Name = "Links House";
        private const string Description = "You are in your house, as it is in the hollow trunk of the tree the room is small and round, and very wooden. There is a small table in the center of the room. The front door leads to the Kokiri forest to the north.";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.North));

            room.AddItem(new Shield().Instantiate());
            room.AddItem(new Table().Instantiate());
            room.AddItem(new Sword().Instantiate());
            room.AddItem(new YoshiDoll().Instantiate());

            return room;
        }

        #endregion
    }
}
