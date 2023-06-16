using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Bathroom : RoomTemplate<Bathroom>
    {
        #region Constants

        private const string Name = "Bathroom";
        private const string Description = "The bathroom is fairly small. There are some clothes drying on a clothes horse. A bath lies along the eastern wall. There is a remarkably clean toilet and sink along the western wall, with a mirror above the sink. To the north is a large window, it is open and you can see out onto the roof of the flat below. The doorway to the south leads into the Western Hallway.";

        #endregion

        #region Overrides of RoomTemplate<Bathroom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South));

            room.AddItem(Bath.Create());
            room.AddItem(Toilet.Create());
            room.AddItem(Mirror.Create());

            return room;
        }

        #endregion
    }
}
