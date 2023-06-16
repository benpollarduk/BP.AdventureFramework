using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class EasternHallway : RoomTemplate<EasternHallway>
    {
        #region Constants

        private const string Name = "Eastern Hallway";
        private const string Description = "The hallway is pretty narrow, and all the walls are bare except for a strange looking telephone. To the east is the front door, but it looks to heavy to open. To the south is the bedroom, to the west the hallway continues.";

        #endregion

        #region Overrides of RoomTemplate<EasternHallway>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.East, true), new Exit(Direction.West));

            room.AddItem(Telephone.Create());

            return room;
        }

        #endregion
    }
}
