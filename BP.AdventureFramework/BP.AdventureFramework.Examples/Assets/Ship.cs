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

            // L2
            regionMaker[0, 0, 0] = new Room(BridgeCentral, "The central bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.South));
            regionMaker[-1, 0, 0] = new Room(BridgePort, "The port side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East));
            regionMaker[1, 0, 0] = new Room(BridgeStarbord, "The starboard side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.West));
            regionMaker[0, -1, 0] = new Room(BridgeTunnel, "The tunnel leads up to the bridge.", new Exit(Direction.North), new Exit(Direction.Down));
            
            // L1
            regionMaker[0, -1, -1] = new Room("Access", "", new Exit(Direction.South), new Exit(Direction.Up));
            regionMaker[0, -2, -1] = new Room("Ambilical", "", new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West));
            regionMaker[0, -3, -1] = new Room("Center FW", "", new Exit(Direction.South), new Exit(Direction.North), new Exit(Direction.Down));
            regionMaker[0, -4, -1] = new Room("Booster", "", new Exit(Direction.North));
            regionMaker[-1, -2, -1] = new Room("Port Wing", "", new Exit(Direction.East));
            regionMaker[1, -2, -1] = new Room("Starboard Wing", "", new Exit(Direction.West));
            regionMaker[-2, -3, -1] = new Room("Port Wing Outer", "", new Exit(Direction.East));
            regionMaker[-1, -3, -1] = new Room("Port Wing Inner", "", new Exit(Direction.East), new Exit(Direction.West));
            regionMaker[1, -3, -1] = new Room("Starboard Wing Inner", "", new Exit(Direction.East), new Exit(Direction.West));
            regionMaker[2, -3, -1] = new Room("Starboard Wing Outer", "", new Exit(Direction.West));

            // L0
            regionMaker[0, -3, -2] = new Room("Central Hull", "", new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
            regionMaker[-1, -3, -2] = new Room("Port Hull", "", new Exit(Direction.East));
            regionMaker[1, -3, -2] = new Room("Starboard Hull", "", new Exit(Direction.West));

            return regionMaker.Make(0, -2, -1);
        }
    }
}
