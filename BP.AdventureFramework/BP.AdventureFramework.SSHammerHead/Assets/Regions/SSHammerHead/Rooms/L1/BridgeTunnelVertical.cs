using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class BridgeTunnelVertical : RoomTemplate<BridgeTunnelVertical>
    {
        #region Constants

        private const string Name = "Bridge Tunnel (Vertical)";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<BridgeTunnelVertical>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.Up));
        }

        #endregion
    }
}
