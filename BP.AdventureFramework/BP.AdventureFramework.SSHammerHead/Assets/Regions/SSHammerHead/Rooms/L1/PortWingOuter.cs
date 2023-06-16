using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class PortWingOuter : RoomTemplate<PortWingOuter>
    {
        #region Constants

        private const string Name = "Port Wing OUter";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<PortWingOuter>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.East));
        }

        #endregion
    }
}
