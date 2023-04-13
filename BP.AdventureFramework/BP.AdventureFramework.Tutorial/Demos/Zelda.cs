using System;
using System.Threading;
using BP.AdventureFramework.Characters;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.GameStructure;
using BP.AdventureFramework.Interaction;
using BP.AdventureFramework.Locations;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.Frames;

namespace BP.AdventureFramework.Tutorial.Demos
{
    public static class Zelda
    {
        private const string Sword = "SWORD";
        private const string Shield = "SHIELD";
        private const string Rupee = "RUPEE";
        private const string TailKey = "TAIL KEY";
        private const string TailCave = "TAIL CAVE";

        public static PlayableCharacter GeneratePC()
        {
            var character = new PlayableCharacter("Link".ToIdentifier(), "A Kokiri boy from the forest".ToDescription())
            {
                Examination = t =>
                {
                    var frame = ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Link"], Console.WindowWidth, Console.WindowHeight);
                    FrameDrawer.DisplaySpecialFrame(frame);
                    return new ExaminationResult(string.Empty, ExaminationResults.SelfContained);
                }
            };

            var shield = new Item("Shield".ToIdentifier(), "A small wooden shield. It has the Deku mark painted on it in red, the sign of the forest.".ToDescription(), true);
            character.AquireItem(shield);

            return character;
        }

        public static Overworld GenerateOverworld(PlayableCharacter pC)
        {
            var overworld = new Overworld("Hyrule".ToIdentifier(), "The ancient land of Hyrule".ToDescription());
            var region = new Region("Kokiri Forest".ToIdentifier(), "The home of the Kokiri tree folk".ToDescription());
            var room = new Room("Link house".ToIdentifier(), new Description("You are in your house, as it is in the hollow trunk of the tree the room is small and round, and very wooden. There is a small table in the center of the room. The front door leads to the Kokiri forest to the north"), new Exit(CardinalDirection.North));

            room.AddItem(new Item("Table".ToIdentifier(), "A small wooden table made from a slice of a trunk of a Deku tree. Pretty handy, but you can't take it with you".ToDescription(), false));

            var sword = new Item("Sword".ToIdentifier(), "A small sword handed down by the Kokiri. It has a wooden handle but the blade is sharp".ToDescription(), true)
            {
                Examination = target =>
                {
                    var frame = ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Sword"], Console.WindowWidth, Console.WindowHeight, 20);
                    FrameDrawer.DisplaySpecialFrame(frame);
                    return new ExaminationResult(string.Empty, ExaminationResults.SelfContained);
                }
            };

            room.AddItem(sword);

            var yoshiDoll = new Item("Yoshi Doll".ToIdentifier(), "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint...".ToDescription(), false)
            {
                Examination = t =>
                {
                    // create an animation frame
                    var frame = new ASCIIAnimationFrame(Timeout.Infinite, 125, false,
                        ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario1"], Console.WindowWidth, Console.WindowHeight, -20),
                        ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario2"], Console.WindowWidth, Console.WindowHeight, -20),
                        ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario3"], Console.WindowWidth, Console.WindowHeight, -20));

                    FrameDrawer.DisplaySpecialFrame(frame);
                    return new ExaminationResult(string.Empty, ExaminationResults.SelfContained);
                }
            };

            room.AddItem(yoshiDoll);

            var outsideLinksHouse = new Room("Outside Links House".ToIdentifier(), new Description("The Kokiri forest looms in front of you. It seems duller and much smaller than you remember, with thickets of deku scrub growing in every direction, except to the north where you can hear the trickle of a small stream. To the south is you house, and to the east is the entrance to the Tail Cave"), new Exit(CardinalDirection.South), new Exit(CardinalDirection.North), new Exit(CardinalDirection.East, true));
            var key01 = new Item("Tail Key".ToIdentifier(), "A small key, with a complex handle in the shape of a worm like creature".ToDescription(), true);
            var saria = new NonPlayableCharacter("Saria".ToIdentifier(), "A very annoying, but admittedly quite pretty elf, dressed, like you, completely in green".ToDescription());

            saria.AquireItem(key01);

            saria.Conversation = new Conversation("Hi Link, how's it going", "I lost my red rupee, if you find it will you please bring it to me?", "Oh Link you are so adorable", "OK Link your annoying me now, I'm just going to ignore you") { RepeatLastElement = true };

            // define the interaction with items for Saria
            saria.Interaction = (item, target) =>
            {
                switch (item.Identifier.IdentifiableName)
                {
                    case Rupee:

                        pC.Give(item, saria);
                        saria.Give(key01, pC);
                        return new InteractionResult(InteractionEffect.SelfContained, item, "Saria looks excited! \"Thanks Link, here take the Tail Key!\"You got the Tail Key, awesome!");

                    case Shield:

                        return new InteractionResult(InteractionEffect.NoEffect, item, "Saria looks at your shield, but seems pretty unimpressed.");

                    case Sword:

                        saria.Kill();

                        if (!saria.HasItem(key01))
                            return new InteractionResult(InteractionEffect.SelfContained, item, "You strike Saria in the face with the sword and she falls down dead.");

                        saria.DequireItem(key01);
                        outsideLinksHouse.AddItem(key01);

                        return new InteractionResult(InteractionEffect.SelfContained, item, "You strike Saria in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");

                    default:

                        return new InteractionResult(InteractionEffect.NoEffect, item);
                }
            };

            outsideLinksHouse.AddCharacter(saria);

            var blockOfWood = new Item("Stump".ToIdentifier(), "A small stump of wood".ToDescription(), false);

            blockOfWood.Interaction = (item, target) =>
            {
                // select by name
                switch (item.Identifier.IdentifiableName)
                {
                    case Shield:

                        return new InteractionResult(InteractionEffect.NoEffect, item, "You hit the stump, and it makes a solid knocking noise");

                    case Sword:

                        blockOfWood.Morph(new Item("Splinters of wood".ToIdentifier(), "Some splinters of wood left from your chopping frenzy on the stump".ToDescription(), false));
                        return new InteractionResult(InteractionEffect.ItemMorphed, item, "You chop the stump into tiny pieces in a mad rage. All that is left is some splinters of wood");

                    default:

                        return new InteractionResult(InteractionEffect.NoEffect, item);
                }
            };

            outsideLinksHouse.AddItem(blockOfWood);

            var tailDoor = new Item("Tail Door".ToIdentifier(), "The doorway to the tail cave".ToDescription(), false);

            outsideLinksHouse.AddItem(tailDoor);

            tailDoor.Interaction = (item, target) =>
            {
                switch (item.Identifier.IdentifiableName)
                {
                    case TailKey:

                        region.UnlockDoorPair(CardinalDirection.East);
                        outsideLinksHouse.RemoveItemFromRoom(tailDoor);
                        return new InteractionResult(InteractionEffect.ItemUsedUp, item, "The Tail Key fits perfectly in the lock, you turn it and the door swings open, revealing a gaping cave mouth...");

                    case Sword:

                        return new InteractionResult(InteractionEffect.NoEffect, item, "Clang clang!");

                    default:
                        return new InteractionResult(InteractionEffect.NoEffect, item);
                }
            };

            var tailCave = new Room("Tail Cave".ToIdentifier(), new Description("The cave is dark, and currently very empty. Quite shabby really, not like the cave on Koholint at all..."), new Exit(CardinalDirection.West, true));
            var stream = new Room("Stream".ToIdentifier(), new Description(string.Empty), new Exit(CardinalDirection.South));
            stream.Description = new ConditionalDescription("A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", "A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest", () => stream.ContainsItem("Bush"));

            var bush = new Item("Bush".ToIdentifier(), "The bush is small, but very dense. Something is gleaming inside, but you cant reach it because the bush is so thick".ToDescription(), false);
            var rupee = new Item("Rupee".ToIdentifier(), "A red rupee! Wow this thing is worth 10 normal rupees".ToDescription(), true) { IsPlayerVisible = false };

            bush.Interaction = (item, target) =>
            {
                switch (item.Identifier.IdentifiableName)
                {
                    case Sword:

                        bush.Morph(new Item("Stump".ToIdentifier(), "A small, hacked up stump from where the bush once was, until you decimated it".ToDescription(), false));
                        rupee.IsPlayerVisible = true;
                        return new InteractionResult(InteractionEffect.ItemMorphed, item, "You slash wildly at the bush and reduce it to a stump. This exposes a red rupee, that must have been what was glinting from within the bush...");

                    default:

                        return new InteractionResult(InteractionEffect.NoEffect, item);
                }
            };

            stream.AddItem(bush);
            stream.AddItem(rupee);

            // add all rooms to region
            region.CreateRoom(room, 0, 0);
            region.CreateRoom(outsideLinksHouse, 0, 1);
            region.CreateRoom(tailCave, 1, 1);
            region.CreateRoom(stream, 0, 2);

            // set start room to first room
            region.SetStartRoom(0);

            // add region to overworld
            overworld.CreateRegion(region, 0, 0);

            return overworld;
        }

        /// <summary>
        /// Determine if the game has completed.
        /// </summary>
        /// <param name="game">The Game to check for completion.</param>
        /// <returns>True if the Game is complete, else false.</returns>
        public static bool DetermineIfGameHasCompleted(Game game)
        {
            return TailCave.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom);
        }
    }
}
