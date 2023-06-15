using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class LinksHouse : RoomTemplate<LinksHouse>
    {
        #region Constants

        private const string Name = "Links House";
        private const string Description = "You are in your house, as it is in the hollow trunk of the tree the room is small and round, and very wooden. There is a small table in the center of the room. The front door leads to the Kokiri forest to the north.";
        private const string YoshiDoll = "Yoshi Doll";
        private const string Table = "Table";
        internal const string Sword = "Sword";
        internal const string Shield = "Shield";

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.North));

            room.AddItem(new Item(Shield, "A small wooden shield. It has the Deku mark painted on it in red, the sign of the forest.", true));
            room.AddItem(new Item(Table, "A small wooden table made from a slice of a trunk of a Deku tree. Pretty handy, but you can't take it with you."));
            room.AddItem(new Item(Sword, "A small sword handed down by the Kokiri. It has a wooden handle but the blade is sharp.", true));
            room.AddItem(new Item(YoshiDoll, "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint..."));

            return room;
        }

        #endregion
    }
}
