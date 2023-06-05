using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Regions.SSHammerHead.Rooms
{
    internal class SupplyRoom : RoomTemplate<SupplyRoom>
    {
        #region Constants

        private const string Name = "Supply Room";
        private const string Description = "The supply room is the rough shape and size of the air lock, but has been used by the crew as a makeshift supply room.";
        private const string Map = "Map";

        #endregion

        #region StaticMethods

        private static CustomCommand[] CreateMapCommands()
        {
            var checkCommand = new CustomCommand(new CommandHelp("Check", "Check the map in detail."), true, (game, arguments) =>
            {
                game.Overworld.CurrentRegion.VisibleWithoutDiscovery = true;
                return new Reaction(ReactionResult.OK, "You check the map in detail. You know understand the internal layout of the ship.");
            });

            return new[] { checkCommand };
        }

        private static Item CreateMap()
        {
            return new Item(Map, "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red. Above the green button is written \"Enter\" and above the red \"Exit\".") { Commands = CreateMapCommands() };
        }

        #endregion

        #region Overrides of RoomTemplate<EngineRoom>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.West));
            room.AddItem(CreateMap());
            return room;
        }

        #endregion
    }
}
