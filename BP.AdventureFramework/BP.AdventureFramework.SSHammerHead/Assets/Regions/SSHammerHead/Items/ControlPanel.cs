using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Items
{
    public class ControlPanel : ItemTemplate<ControlPanel>
    {
        #region Constants

        internal const string Name = "Control Panel";
        private const string Description = "A small wall mounted control panel. Written on the top of the panel in a formal font are the words \"Airlock Control\". It has two buttons, green and red. Above the green button is written \"Enter\" and above the red \"Exit\".";

        #endregion

        #region StaticMethods

        private static CustomCommand[] CreateControlPannelCommands(PlayableCharacter pC, Room room)
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

        #endregion

        #region Overrides of ItemTemplate<ControlPanel>

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <param name="room">The room.</param>
        /// <returns>The item.</returns>
        protected override Item OnCreate(PlayableCharacter pC, Room room)
        {
            var controlPanel = new Item(Name, Description) { Commands = CreateControlPannelCommands(pC, room) };

            controlPanel.Interaction = (item, target) =>
            {
                if (Name.EqualsExaminable(controlPanel))
                {
                    if (Hammer.Name.EqualsIdentifier(item.Identifier))
                    {
                        room.RemoveItem(controlPanel);
                        room.AddItem(BrokenControlPanel.Create());
                        return new InteractionResult(InteractionEffect.ItemMorphed, item, $"Smalling the {Hammer.Name} in to the control panel causes it to hiss and smoke pours out. Other than the odd spark it is now lifeless.");
                    }
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return controlPanel;
        }

        #endregion
    }
}
