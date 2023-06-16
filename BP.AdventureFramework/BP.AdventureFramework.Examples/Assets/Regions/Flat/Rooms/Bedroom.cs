using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Bedroom : RoomTemplate<Bedroom>
    {
        #region Constants

        private const string Name = "Bedroom";
        private const string Description = "The bedroom is large, with one duck-egg blue wall.There is a double bed against the western wall, and a few other items of bedroom furniture are dotted around, but they all look pretty scruffy.To the north is a doorway leading to the hallway.";

        #endregion

        #region Overrides of RoomTemplate<Bedroom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            var room = new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.Up));

            room.AddItem(Bed.Create());
            room.AddItem(Picture.Create());
            room.AddItem(TV.Create());

            return room;
        }

        #endregion
    }
}
