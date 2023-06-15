using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Conversations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class OutsideLinksHouse : RoomTemplate<OutsideLinksHouse>
    {
        #region Constants

        private const string Name = "Outside Links House";
        private const string Description = "The Kokiri forest looms in front of you. It seems duller and much smaller than you remember, with thickets of deku scrub growing in every direction, except to the north where you can hear the trickle of a small stream. To the south is you house, and to the east is the entrance to the Tail Cave.";
        private const string Saria = "Saria";
        private const string TailKey = "Tail Key";
        internal const string Stump = "Stump";
        internal const string SplintersOfWood = "Splinters Of Wood";

        internal const string TailDoor = "Tail Door";

        #endregion

        #region StaticMethods

        private static NonPlayableCharacter CreateSaria(PlayableCharacter pC, Room room)
        {
            var key01 = new Item(TailKey, "A small key, with a complex handle in the shape of a worm like creature", true);
            var saria = new NonPlayableCharacter(Saria, "A very annoying, but admittedly quite pretty elf, dressed, like you, completely in green");

            saria.AquireItem(key01);

            saria.Conversation = new Conversation
            (
                new Paragraph("Hi Link, how's it going.."),
                new Paragraph("I lost my red rupee, if you find it will you please bring it to me?"),
                new Paragraph("Oh Link you are so adorable"),
                new Paragraph("OK Link your annoying me now, I'm just going to ignore you.", 0)
            );

            saria.Interaction = (item, target) =>
            {
                if (Stream.Rupee.EqualsIdentifier(item.Identifier))
                {
                    pC.Give(item, saria);
                    saria.Give(key01, pC);
                    return new InteractionResult(InteractionEffect.SelfContained, item, $"{Saria} looks excited! \"Thanks Link, here take the Tail Key!\" You've got the Tail Key, awesome!");
                }

                if (LinksHouse.Shield.EqualsIdentifier(item.Identifier))
                {
                    return new InteractionResult(InteractionEffect.NoEffect, item, $"{Saria} looks at your shield, but seems pretty unimpressed.");
                }

                if (LinksHouse.Sword.EqualsIdentifier(item.Identifier))
                {
                    saria.Kill(string.Empty);

                    if (!saria.HasItem(key01))
                        return new InteractionResult(InteractionEffect.SelfContained, item, $"You strike {Saria} in the face with the sword and she falls down dead.");

                    saria.DequireItem(key01);
                    room.AddItem(key01);

                    return new InteractionResult(InteractionEffect.SelfContained, item, $"You strike {Saria} in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return saria;
        }

        private static Item CreateStump()
        {
            var stump = new Item(Stump, "A small stump of wood");

            stump.Interaction = (item, target) =>
            {
                if (LinksHouse.Shield.EqualsExaminable(item))
                {
                    return new InteractionResult(InteractionEffect.NoEffect, item, "You hit the stump, and it makes a solid knocking noise");
                }

                if (LinksHouse.Sword.EqualsExaminable(item))
                {
                    stump.Morph(new Item(SplintersOfWood, "Some splinters of wood left from your chopping frenzy on the stump"));
                    return new InteractionResult(InteractionEffect.ItemMorphed, item, "You chop the stump into tiny pieces in a mad rage. All that is left is some splinters of wood");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return stump;
        }

        private static Item CreateTailDoor(Room room)
        {
            var tailDoor = new Item(TailDoor, "The doorway to the tail cave");

            tailDoor.Interaction = (item, target) =>
            {
                if (TailKey.EqualsExaminable(item))
                {
                    if (room.FindExit(Direction.East, true, out var exit))
                        exit.Unlock();

                    room.RemoveItem(tailDoor);
                    return new InteractionResult(InteractionEffect.ItemUsedUp, item, "The Tail Key fits perfectly in the lock, you turn it and the door swings open, revealing a gaping cave mouth...");
                }

                if (LinksHouse.Sword.EqualsExaminable(item))
                {
                    return new InteractionResult(InteractionEffect.NoEffect, item, "Clang clang!");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return tailDoor;
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
            var room = new Room(Name, Description, new Exit(Direction.South), new Exit(Direction.North), new Exit(Direction.East, true));

            room.AddCharacter(CreateSaria(pC, room));
            room.AddItem(CreateStump());
            room.AddItem(CreateTailDoor(room));
            
            return room;
        }

        #endregion
    }
}
