using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.NPCs;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class OutsideLinksHouse : RoomTemplate<OutsideLinksHouse>
    {
        #region Constants

        private const string Name = "Outside Links House";
        private const string Description = "The Kokiri forest looms in front of you. It seems duller and much smaller than you remember, with thickets of deku scrub growing in every direction, except to the north where you can hear the trickle of a small stream. To the south is you house, and to the east is the entrance to the Tail Cave.";

        #endregion

        #region Overrides of RoomTemplate<OutsideLinksHouse>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.North), new Exit(Direction.East, true));
            var door = TailDoor.Create();
            var saria = Saria.Create(pC, room);

            door.Interaction = (item, target) =>
            {
                if (TailKey.Name.EqualsExaminable(item))
                {
                    if (room.FindExit(Direction.East, true, out var exit))
                        exit.Unlock();

                    room.RemoveItem(door);
                    return new InteractionResult(InteractionEffect.ItemUsedUp, item, "The Tail Key fits perfectly in the lock, you turn it and the door swings open, revealing a gaping cave mouth...");
                }

                if (Sword.Name.EqualsExaminable(item))
                    return new InteractionResult(InteractionEffect.NoEffect, item, "Clang clang!");

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            room.AddItem(Stump.Create());
            room.AddItem(door);
            room.AddCharacter(saria);

            return room;
        }

        #endregion
    }
}
