using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.SSHammerhead;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets
{
    internal static class Ship
    {
        // rooms
        private const string ShipName = "SS Hammerhead";
        private const string BridgeCentral = "Bridge (central)";
        private const string BridgePort = "Bridge (port)";
        private const string BridgeStarbord = "Bridge (starboard)";
        private const string BridgeTunnel = "Bridge Tunnel";
        private const string BridgeTunnelVertical = "Bridge Tunnel (vertical)";
        private const string BridgeTunnelEntry = "Bridge Tunnel (entry)";
        private const string CentralHull = "Central Hull";
        private const string StarboardHull = "Starboard Hull";
        private const string PortWing = "Port Wing";
        private const string PortWingInner = "Port Wing Inner";
        private const string PortWingOuter = "Port Wing Outer";
        private const string StarboardWing = "Starboard Wing";
        private const string StarboardWingInner = "Starboard Wing Inner";
        private const string StarboardWingOuter = "Starboard Wing Outer";
        private const string Booster = "Booster";
        private const string EngineRoom = "Engine Room";

        // characters

        internal static Region GenerateRegion(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(ShipName, "The SS Hammerhead");

            // L2
            var bridgeCentral = new Room(BridgeCentral, "The central bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.South));
            var bridgePort = new Room(BridgePort, "The port side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East));
            var bridgeStarbord = new Room(BridgeStarbord, "The starboard side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.West));
            var bridgeTunnel = new Room(BridgeTunnel, "The tunnel leads up to the bridge.", new Exit(Direction.North), new Exit(Direction.Down));

            // L1
            var bridgeTunnelVertical = new Room(BridgeTunnelVertical, "", new Exit(Direction.South), new Exit(Direction.Up));
            var bridgeTunnelEntry = new Room(BridgeTunnelEntry, "", new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West));
            var centralHull = new Room(CentralHull, "", new Exit(Direction.South), new Exit(Direction.North), new Exit(Direction.Down));
            var booster = new Room(Booster, "", new Exit(Direction.North));
            var portWing = new Room(PortWing, "", new Exit(Direction.East));
            var starboardWing = new Room(StarboardWing, "", new Exit(Direction.West));
            var portWingOuter = new Room(PortWingOuter, "", new Exit(Direction.East));
            var portWingInner = new Room(PortWingInner, "", new Exit(Direction.East), new Exit(Direction.West));
            var starboardWingInner = new Room(StarboardWingInner, "", new Exit(Direction.East), new Exit(Direction.West));
            var starboardWingOuter = new Room(StarboardWingOuter, "", new Exit(Direction.West));

            // L0
            var engineRoom = new Room(EngineRoom, "", new Exit(Direction.Up), new Exit(Direction.East), new Exit(Direction.West));
            var starboardHull = new Room(StarboardHull, "", new Exit(Direction.West));
            
            // assign room

            // L2
            regionMaker[0, 0, 0] = bridgeCentral;
            regionMaker[-1, 0, 0] = bridgePort;
            regionMaker[1, 0, 0] = bridgeStarbord;
            regionMaker[0, -1, 0] = bridgeTunnel;
            
            // L1
            regionMaker[0, -1, -1] = bridgeTunnelVertical;
            regionMaker[0, -2, -1] = bridgeTunnelEntry;
            regionMaker[0, -3, -1] = centralHull;
            regionMaker[0, -4, -1] = booster;
            regionMaker[-1, -2, -1] = portWing;
            regionMaker[1, -2, -1] = starboardWing;
            regionMaker[-2, -3, -1] = portWingOuter;
            regionMaker[-1, -3, -1] = portWingInner;
            regionMaker[1, -3, -1] = starboardWingInner;
            regionMaker[2, -3, -1] = starboardWingOuter;

            // L0
            regionMaker[0, -3, -2] = engineRoom;
            regionMaker[-1, -3, -2] = Airlock.Create(pC);
            regionMaker[1, -3, -2] = starboardHull;

            // start in airlock
            return regionMaker.Make(-1, -3, -2);
        }
    }
}
