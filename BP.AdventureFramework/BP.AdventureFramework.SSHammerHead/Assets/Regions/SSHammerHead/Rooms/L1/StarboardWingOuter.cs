using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class StarboardWingOuter : RoomTemplate<StarboardWingOuter>
    {
        #region Constants

        private const string Name = "Starboard Wing Outer";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<StarboardWingOuter>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
