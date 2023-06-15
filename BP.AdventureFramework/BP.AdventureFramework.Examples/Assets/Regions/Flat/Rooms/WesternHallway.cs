using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class WesternHallway : RoomTemplate<WesternHallway>
    {
        #region Constants

        private const string Name = "Western Hallway";
        private const string Description = "This hallway is a continuation of the Eastern Hallway, to the north is the Bathroom, to the west is the Kitchen, to the South is a neat looking Spare Room. The hallway continues to the East.";

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
