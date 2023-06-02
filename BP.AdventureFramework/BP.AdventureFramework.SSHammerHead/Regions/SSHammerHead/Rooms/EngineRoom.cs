using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Regions.SSHammerHead.Rooms
{
    internal class EngineRoom : RoomTemplate<EngineRoom>
    {
        #region Constants

        private const string Name = "Engine Room";
        private const string Description = "This room hosts the large engine that used to power the SS HammerHead. It is now dormant and eerily silent, the fusion mechanism long since powered down. The room itself is very industrial, with metal walkways surrounding the perimeter of the room and the engine itself. A ladder leads upwards from one of these walkways.";

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            return new Room(Name, Description, new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
