using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Examples.Assets.SSHammerhead
{
    internal class EngineRoom : RoomTemplate<EngineRoom>
    {
        #region Constants

        private const string Name = "Engine Room";
        private const string Description = "The airlock is a small, mostly empty, chamber with two thick doors.One leads in to the ship, the other back to deep space.";

        #endregion

        #region Overrides of RoomTemplate<Airlock2>

        protected override Room OnCreate(PlayableCharacter pC)
        {
            return new Room(Name, Description, new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
        }

        #endregion
    }
}
