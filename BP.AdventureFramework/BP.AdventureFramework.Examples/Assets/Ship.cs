using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets
{
    internal static class Ship
    {
        private const string ShipName = "SS Ben";
        private const string BridgeCentral = "Bridge (central)";
        private const string BridgePort = "Bridge (port)";
        private const string BridgeStarbord = "Bridge (starboard)";
        private const string BridgeTunnel = "Tunnel";
        private const string BridgeTunnelVertical = "Tunnel";
        private const string BridgeTunnelEntry = "Tunnel entry";
        private const string Hull = "Hull";

        internal static Region GenerateRegion(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(ShipName, "The SS Ben");

            regionMaker[0, 0, 0] = new Room(BridgeCentral, "The central bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.South));
            regionMaker[-1, 0, 0] = new Room(BridgePort, "The port side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East));
            regionMaker[1, 0, 0] = new Room(BridgeStarbord, "The starboard side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.West));
            regionMaker[0, -1, 0] = new Room(BridgeTunnel, "The tunnel leads up to the bridge.", new Exit(Direction.North), new Exit(Direction.Down));
            regionMaker[0, -1, -1] = new Room(BridgeTunnelVertical, "The tunnel leads up to the bridge.", new Exit(Direction.Up), new Exit(Direction.Down));
            regionMaker[0, -1, -2] = new Room(BridgeTunnelEntry, "The entry to the tunnel.", new Exit(Direction.Up), new Exit(Direction.South));
            regionMaker[0, -2, -2] = new Room(Hull, "The underbelly of the ship.", new Exit(Direction.South));

            return regionMaker.Make(0, 0, 0);
        }
    }
}
