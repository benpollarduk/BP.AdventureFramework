using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2
{
    internal class BridgeTunnel : RoomTemplate<BridgeTunnel>
    {
        #region Constants

        private const string Name = "Bridge Tunnel";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<BridgeTunnel>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.Down));
        }

        #endregion
    }
}
