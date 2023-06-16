using System.Linq;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L0;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1;
using BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead
{
    internal class SSHammerHead : RegionTemplate<SSHammerHead>
    {
        #region Constants

        private const string Name = "SS Hammerhead.";
        private const string Description = "The star ship Hammerhead.";

        #endregion

        #region Overrides of RegionTemplate<SSHammerHead>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                // L2
                [0, 0, 0] = Bridge.Create(),
                [-1, 0, 0] = BridgePort.Create(),
                [1, 0, 0] = BridgeStarboard.Create(),
                [0, -1, 0] = BridgeTunnel.Create(),
                // L1
                [0, -1, -1] = BridgeTunnelVertical.Create(),
                [0, -2, -1] = BridgeTunnelEntry.Create(),
                [0, -3, -1] = CentralHull.Create(),
                [0, -4, -1] = Booster.Create(),
                [-1, -2, -1] = PortWing.Create(),
                [1, -2, -1] = StarboardWing.Create(),
                [-2, -3, -1] = PortWingOuter.Create(),
                [-1, -3, -1] = PortWingInner.Create(),
                [1, -3, -1] = StarboardWingInner.Create(),
                [2, -3, -1] = StarboardWingOuter.Create(),
                // L0
                [0, -3, -2] = EngineRoom.Create(),
                [-1, -3, -2] = Airlock.Create(pC),
                [1, -3, -2] = SupplyRoom.Create(pC)
            };

            // start in airlock
            return regionMaker.Make(regionMaker.GetRoomPositions().FirstOrDefault(r => Airlock.Name.EqualsIdentifier(r.Room.Identifier)));
        }

        #endregion
    }
}
