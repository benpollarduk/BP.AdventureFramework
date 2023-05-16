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

        internal static Region GenerateRegion(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(ShipName, "The SS Ben");

            regionMaker[0, 0, 0] = new Room(BridgeCentral, "The central bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.South));
            regionMaker[-1, 0, 0] = new Room(BridgePort, "The port side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.East));
            regionMaker[1, 0, 0] = new Room(BridgeStarbord, "The starboard side of the bridge is full of consoles filled with all kinds of dials, knobs and buttons.", new Exit(Direction.West));

            return regionMaker.Make(0, 0, 0);
        }
    }
}
