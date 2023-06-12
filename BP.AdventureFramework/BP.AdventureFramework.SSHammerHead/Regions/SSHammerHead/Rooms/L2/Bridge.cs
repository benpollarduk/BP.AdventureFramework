using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Regions.SSHammerHead.Rooms.L2
{
    internal class Bridge : RoomTemplate<Bridge>
    {
        #region Constants

        private const string Name = "Bridge";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            return new Room(Name, Description, new Exit(Direction.West), new Exit(Direction.East), new Exit(Direction.South));
        }

        #endregion
    }
}
