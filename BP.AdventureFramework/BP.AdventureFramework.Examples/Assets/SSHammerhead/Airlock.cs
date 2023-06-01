using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;

namespace BP.AdventureFramework.Examples.Assets.SSHammerhead
{
    internal class Airlock : RoomTemplate<Airlock>
    {
        private const string Name = "Airlock";
        private const string Description = "The airlock is a small, mostly empty, chamber with two thick doors.One leads in to the ship, the other back to deep space.";
        private const string ControlPanel = "Control Panel";
        private const string BrokenControlPanel = "Broken Control Panel";

        private CustomCommand[] CreateControlPannelCommands(PlayableCharacter pC, Room room)
        {
            var redButtonCommand = new CustomCommand(new CommandHelp("Press red", "Press the red button on the control panel."), true, (game, arguments) =>
            {
                room.FindExit(Direction.West, true, out var west);
                west.Unlock();
                const string result = "You press the red button on the control panel. The airlock door that leads to outer space opens and in an instant you are sucked out. As you drift in to outer space the SS Hammerhead becomes smaller and smaller until you can no longer see it. You die all alone.";
                pC.Kill(result);
                return new Reaction(ReactionResult.Fatal, result);
            });

            var greenButtonCommand = new CustomCommand(new CommandHelp("Press green", "Press the green button on the control panel."), true, (game, arguments) =>
            {
                room.FindExit(Direction.East, true, out var east);
                east.Unlock();
                return new Reaction(ReactionResult.OK, "You press the green button on the control panel. The airlock door that leads to The SS Hammerhead opens.");
            });

            return new[] { redButtonCommand, greenButtonCommand };
        }

        private Item CreateControlPanel(PlayableCharacter pC, Room room)
        {
            var controlPanel = new Item(ControlPanel, "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red.") { Commands = CreateControlPannelCommands(pC, room) };

            controlPanel.Interaction = (item, target) =>
            {
                if (ControlPanel.EqualsExaminable(controlPanel))
                {
                    if (Everglades.Knife.EqualsExaminable(item))
                    {
                        controlPanel.Morph(new Item(BrokenControlPanel, "The beaten up and broken remains of a control panel."));
                        controlPanel.Commands = null;
                        return new InteractionResult(InteractionEffect.ItemMorphed, item, $"Jabbing the {Everglades.Knife} in to the control panel causes it to hiss and smoke pours out. Other than the odd spark it is now lifeless.");
                    }
                }
                else if (BrokenControlPanel.EqualsExaminable(controlPanel))
                {
                    if (Everglades.Knife.EqualsExaminable(item))
                    {
                        return new InteractionResult(InteractionEffect.FatalEffect, item, $"Once again you jab the {Everglades.Knife} in to the remains of the control panel. You must have hit a high voltage wire inside because you are suddenly electrocuted. You are electrocuted to death.");
                    }
                }
                
                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return controlPanel;
        }

        #region Overrides of RoomTemplate<Airlock2>

        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.East, true), new Exit(Direction.West, true));
            room.AddItem(CreateControlPanel(pC, room));
            return room;
        }

        #endregion
    }
}
