using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.SSHammerHead.Regions.SSHammerHead.Rooms
{
    internal class SupplyRoom : RoomTemplate<SupplyRoom>
    {
        #region Constants

        private const string Name = "Supply Room";
        private const string Description = "The supply room is the rough shape and size of the air lock, but has been used by the crew as a makeshift supply room, containing everything from spare parts for the ship to first aid kits.";
        private const string Blueprint = "Blueprint";
        private const string Tray = "Tray";
        private const string EmptyTray = "Empty Tray";
        private const string USBDrive = "USB Drive";

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

        private static Item CreateBlueprint()
        {
            return new Item(Blueprint, "A wall mounted blueprint of the ship, entitle SS Hammerhead. The blueprint shows the ship from various angles with details of each area.") { Commands = CreateMapCommands() };
        }
        
        private static Item CreateUSBDrive()
        {
            return new Item(USBDrive, "A small 32GB USB drive.");
        }

        private static Item CreateTray(PlayableCharacter pC)
        {
            var item = new Item(Tray, "A tray containing a range of different cables that have become intertwined.");

            item.Examination = x =>
            {
                if (Tray.EqualsExaminable(x))
                {
                    var examinedJumble = new Item(EmptyTray, "There is nothing else of interest in the tray.");
                    item.Morph(examinedJumble);
                    pC.AquireItem(CreateUSBDrive());
                    return new ExaminationResult("A tray containing a range of different cables that have become intertwined. Amongst the jumble is a small USB drive, you empty the contents of the tray on to the shelf in front of you. It seems unusual to leave the USB drive here so you take it.", ExaminationResults.DescriptionReturned);
                }

                return new ExaminationResult("There is nothing else of interest in the tray.", ExaminationResults.DescriptionReturned);
            };

            return item;
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
            room.AddItem(CreateBlueprint());
            room.AddItem(CreateTray(pC));
            return room;
        }

        #endregion
    }
}
