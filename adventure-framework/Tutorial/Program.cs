using System;
using System.Threading;

namespace Tutorial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // buffer in game graphics from the \Graphics dir
                InGameGraphics.BufferGraphics();

                // buffer all user chains from the \Chains dir. Place any chains you create in the \Chains dir and this will buffer then into memory
                Chains.BufferUserDefinedChains();

                // setup the console
                setupConsole();

                // create helper
                GameCreationHelper helper = GameCreationHelper.Create("The Legend Of Zelda: Links Texting!",
                    "It's a sunny day in Hyrule and Link is in his tree hut...",
                    new OverworldGeneration(generateOverworld),
                    new PlayerGeneration(generateCharacter),
                    new CompletionCheck(determineIfGameHasCompleted));


                // create new flow for the game
                using (GameFlow flow = new GameFlow(helper))
                {
                    // setup console
                    HostSetup.SetupWindowsConsole(flow, "The Legend Of Zelda: Links Texting!");

                    // begin
                    flow.Begin();
                }
            }
            catch (Exception e)
            {
                // just display
                Console.WriteLine("Exception caught running Tutorial: {0}\nPress enter to close", e.Message);

                // wait for enter to close
                Console.ReadLine();

                // display closing
                Console.WriteLine("Closing...");
            }
        }

        /// <summary>
        /// Setup the console
        /// </summary>
        private static void setupConsole()
        {
            try
            {
                // try and set desired size

                // set window size
                Console.SetWindowSize(80, 50);

                // set buffer size
                Console.SetBufferSize(80, 50);
            }
            catch (ArgumentOutOfRangeException)
            {
                // let console size itself
            }

            // set foreground
            Console.ForegroundColor = ConsoleColor.White;

            // set background
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Generate the PlayableCharacter
        /// </summary>
        /// <returns>The playable character</returns>
        private static PlayableCharacter generateCharacter()
        {
            // create the playable chracater
            PlayableCharacter character = new PlayableCharacter("Link", "A Kokiri boy from the forest");

            // set examination of self
            character.Examination = new ExaminationCallback((IExaminable t) =>
            {
                // create frame
                ASCIIImageFrame frame = ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["link"], Console.WindowWidth, Console.WindowHeight);

                // display frame
                FrameDrawer.DisplaySpecialFrame(frame);

                // just for a demo play the sound
                BeepPlayer.PlayChain(Chains.Attention);

                // return result
                return new ExaminationResult(string.Empty, EExaminationResults.SelfContained);
            });

            // create shield
            Item shield = new Item("Shield", "A small wooden shield. It has the deku mark painted on it in red, the sign of the forest.", true);

            // give to character
            character.AquireItem(shield);

            // return the character
            return character;
        }

        /// <summary>
        /// Generate the Overworld
        /// </summary>
        /// <param name="pC">The playable character that will appear in the overworld</param>
        /// <returns>The fully generated Overworld</returns>
        private static Overworld generateOverworld(PlayableCharacter pC)
        {
            // generate a new overworld
            Overworld overworld = new Overworld("Hyrule", "The ancient land of Hyrule");

            // create a new region
            Region region = new Region("Kokiri Forest", "The home of the Kokiri tree folk");

            // create a new room
            Room room = new Room("Link house", "You are in your house, as it is in the hollow trunk of the tree the room is small and round, and very wooden. There is a small table in the center of the room. The front door leads to the Kokiri forest to the north", new Exit(CardinalDirection.North));

            // add a table
            room.AddItem(new Item("Table", "A small wooden table made from a slice of a trunk of a deku tree. Pretty handy, but you can't take it with you", false));

            // create the sword
            Item sword = new Item("Sword", "A small sword handed down by the kokiri. It has a wooden handle but the blade is sharp", true);

            // set what happens during examination
            sword.Examination = new ExaminationCallback((IExaminable target) =>
            {
                // create an ASCII image from the bitmap
                ASCIIImageFrame frame = ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Sword"], Console.WindowWidth, Console.WindowHeight, 20);

                // display the frame
                FrameDrawer.DisplaySpecialFrame(frame);

                // return result
                return new ExaminationResult(string.Empty, EExaminationResults.SelfContained);
            });

            // put in the room
            room.AddItem(sword);

            // create yoshi doll
            Item yoshiDoll = new Item("Yoshi Doll", "A small mechanical doll in the shape of Yoshi. Apparently these are all the rage on Koholint...", false);

            // set examination of yoshi doll
            yoshiDoll.Examination = new ExaminationCallback((IExaminable t) =>
            {
                // create an animation frame
                ASCIIAnimationFrame frame = new ASCIIAnimationFrame(Timeout.Infinite, 125, false,
                    ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario1"], Console.WindowWidth, Console.WindowHeight, -20),
                    ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario2"], Console.WindowWidth, Console.WindowHeight, -20),
                    ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario3"], Console.WindowWidth, Console.WindowHeight, -20));
                // display frame
                FrameDrawer.DisplaySpecialFrame(frame);

                // return result
                return new ExaminationResult(string.Empty, EExaminationResults.SelfContained);
            });

            // add
            room.AddItem(yoshiDoll);

            // create outside area
            Room outsideLinksHouse = new Room("Outside Links House", "The Kokiri forest looms in front of you. It seems duller and much smaller than you remember, with thickets of deku scrub growing in every direction, except to the north where you can hear the trickle of a small stream. To the south is you house, and to the east is the entrance to the Tail Cave", new Exit(CardinalDirection.South), new Exit(CardinalDirection.North), new Exit(CardinalDirection.East, true));

            // create key
            Item key01 = new Item("Tail Key", "A small key, with a complex handle in the shape of a worm like creature", true);

            // create Saria as a NPC
            NonPlayableCharacter saria = new NonPlayableCharacter("Saria", "A very annoying, but admitedly quite preety elf, dressed, like you, completly in green");

            // add item
            saria.AquireItem(key01);

            // set conversation
            saria.Conversation = new Conversation("Hi Link, how's it going", "I lost my red rupee, if you find it will you please bring it to me?", "Oh Link you are so adorable", "OK Link your annoying me now, I'm just going to ignore you");

            // make her keep repeating the last element
            saria.Conversation.RepeatLastElement = true;

            // define the interaction with items for Saria
            saria.Interaction = new InteractionCallback((Item item, IInteractWithItem target) =>
            {
                // select the item used by name
                switch (item.Name.ToUpper())
                {
                    case "RUPEE":
                        {
                            // give rupee
                            pC.Give(item, saria);

                            // give key
                            saria.Give(key01, pC);

                            // return the result
                            return new InteractionResult(EInteractionEffect.SelfContained, item, "Saria looks excited! \"Thanks Link, here take the Tail Key!\"You got the Tail Key, awesome!");
                        }
                    case "SHIELD":
                        {
                            // return the result of her checking it out
                            return new InteractionResult(EInteractionEffect.NoEffect, item, "Saria looks at your shield, but semms pretty unimpressed. Women!");
                        }
                    case "SWORD":
                        {
                            // kill her!
                            saria.Kill();

                            // if she has key
                            if (saria.HasItem(key01))
                            {
                                // remove from saria
                                saria.DequireItem(key01);

                                // put in room
                                outsideLinksHouse.AddItem(key01);

                                // return the result of hitting her - she drops key
                                return new InteractionResult(EInteractionEffect.SelfContained, item, "You strike Saria in the face with the sword and she falls down dead. When she fell you saw something drop to out of her hand, it looked like a key...");
                            }

                            // return the result of hitting her
                            return new InteractionResult(EInteractionEffect.SelfContained, item, "You strike Saria in the face with the sword and she falls down dead.");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, item);
                        }
                }
            });

            // put Saria in the room
            outsideLinksHouse.AddCharacter(saria);

            // create stump
            Item blockOfWood = new Item("Stump", "A small stump of wood", false);

            // set interaction
            blockOfWood.Interaction = new InteractionCallback((Item item, IInteractWithItem target) =>
            {
                // select by name
                switch (item.Name.ToUpper())
                {
                    case "SHIELD":
                        {
                            // return the result hitting the stump with it
                            return new InteractionResult(EInteractionEffect.NoEffect, item, "You hit the stump, and it makes a solid knocking noise");
                        }
                    case "SWORD":
                        {
                            // morph the block of wood
                            blockOfWood.Morph(new Item("Splinters of wood", "Some splinters of wood left from your chopping frenzy on the stump", false));

                            // return the result
                            return new InteractionResult(EInteractionEffect.ItemMorphed, item, "You chop the stump into tiny pieces in a mad rage. All that is left is some splinters of wood");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, item);
                        }
                }
            });

            // add to room
            outsideLinksHouse.AddItem(blockOfWood);

            // create tail doorway
            Item tailDoor = new Item("Tail Door", "The doorway to the tail cave", false);

            // add to outside area
            outsideLinksHouse.AddItem(tailDoor);

            // set interaction
            tailDoor.Interaction = new InteractionCallback((Item item, IInteractWithItem target) =>
            {
                // select by name
                switch (item.Name.ToUpper())
                {
                    case "TAIL KEY":
                        {
                            // unlock exits
                            region.UnlockDoorPair(CardinalDirection.East);

                            // remove tail door from room
                            outsideLinksHouse.RemoveItemFromRoom(tailDoor);

                            // return the result of using the key
                            return new InteractionResult(EInteractionEffect.ItemUsedUp, item, "The Tail Key fits perfectly in the lock, you turn it and the door swings open, revealing a gaping cave mouth...");
                        }
                    case "SWORD":
                        {
                            // return the result
                            return new InteractionResult(EInteractionEffect.NoEffect, item, "Clang clang!");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, item);
                        }
                }
            });

            // add tail cave
            Room tailCave = new Room("Tail Cave", "The cave is dark, and currently very empty. Quite shabby really, not like the cave on Koholint at all...", new Exit(CardinalDirection.West, true));

            // create stream
            Room stream = new Room("Stream", string.Empty, new Exit(CardinalDirection.South));

            // set a conditional description for the stream depending on wether it is cut down or not
            stream.Description = new ConditionalDescription("A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", "A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest", new Condition(() => stream.ContainsItem("Bush")));

            // create bush
            Item bush = new Item("Bush", "The bush is small, but very dense. Something is gleaming inside, but you cant reach it beacuse the bush is so thick", false);

            // create rupee
            Item rupee = new Item("Rupee", "A red rupee! Wow this thing is worth 10 normal rupees", true);

            // make hidden for now
            rupee.IsPlayerVisible = false;

            // set interaction for the bush
            bush.Interaction = new InteractionCallback((Item item, IInteractWithItem target) =>
            {
                // select by name
                switch (item.Name.ToUpper())
                {
                    case "SWORD":
                        {
                            // morph the bush into stump
                            bush.Morph(new Item("Stump", "A small, hacked up stump from where the bush once was, until you decimated it", false));

                            // reveal the rupee
                            rupee.IsPlayerVisible = true;

                            // return the result
                            return new InteractionResult(EInteractionEffect.ItemMorphed, item, "You slash wildly at the bush and reduce it to a stump. This exposes a red rupee, that must have been what was glinting from within the bush...");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, item);
                        }
                }
            });

            // add bush to room
            stream.AddItem(bush);

            // add rupee to room
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

            // return the overworld
            return overworld;
        }

        /// <summary>
        /// Determine if the game has completed
        /// </summary>
        /// <param name="game">The Game to check for completion</param>
        /// <returns>True if the Game is complete, else false</returns>
        private static bool determineIfGameHasCompleted(Game game)
        {
            // determine the completion condition
            return game.Overworld.CurrentRegion.CurrentRoom.Name.ToUpper() == "TAIL CAVE";
        }
    }
}