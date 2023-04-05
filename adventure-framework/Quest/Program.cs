using System;
using System.Threading;

namespace Quest
{
    internal class Program
    {
        #region StaticMethods

        private static void Main(string[] args)
        {
            try
            {
                // setup the console
                setupConsole();

                // buffer all user defined chains into memory
                Chains.BufferUserDefinedChains();

                // buffer all graphics
                InGameGraphics.BufferGraphics();

                // hold helper
                GameCreationHelper creationHelper = null;

                // loop till selected
                while (creationHelper == null)
                {
                    // clear
                    Console.Clear();

                    // display menu
                    Console.WriteLine("Select Demo Game:");
                    Console.WriteLine("1. Everglades");
                    Console.WriteLine("2. Flat");
                    Console.WriteLine("3. Asteriod");
                    Console.WriteLine("4. Zelda");

                    // select game
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            {
                                // create game helper
                                creationHelper = GameCreationHelper.Create("A Strange World",
                                    "You wake up at the entrance to a small clearing...",
                                    new OverworldGeneration(Program.generateEverglades),
                                    new PlayerGeneration(Program.generateJungleBen),
                                    new CompletionCheck((Game g) => { return false; }));

                                break;
                            }
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            {
                                // create game helper
                                creationHelper = GameCreationHelper.Create("Escape From Bagley House!",
                                    "You wake up in the bedroom of your flat in Bagley house. Your a little disorientated, but then again you are most mornings! Your itching for some punk rock!",
                                    new OverworldGeneration(Program.generateFlat),
                                    new PlayerGeneration(Program.generateBen),
                                    new CompletionCheck((Game g) => { return false; }));

                                break;
                            }
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            {
                                // create game helper
                                creationHelper = GameCreationHelper.Create("A Million Miles Away",
                                    "You have crash landed on an asteriod approximately 1,000,000 miles from earth. Your ship lies in ruins. You are fucked",
                                    new OverworldGeneration(Program.generateNewWorld),
                                    new PlayerGeneration(Program.generateAstronaut),
                                    new CompletionCheck((Game g) => { return false; }));

                                break;
                            }
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            {
                                // create game helper
                                creationHelper = GameCreationHelper.Create("The Legend Of Zelda: Links Texting",
                                    "Princess Zelda has been kidnapped by Gannodorf and is being help hostage somewhere in Hyrule castle. Meanwhile a horny Impa is trying to hunt you down! What will you do - save the girl or play with one?!",
                                    new OverworldGeneration(Program.generateHyrule),
                                    new PlayerGeneration(Program.generateLink),
                                    new CompletionCheck((Game g) => { return false; }));

                                break;
                            }
                    }
                }

                // create new flow for the game
                using (GameFlow flow = new GameFlow(creationHelper))
                {
                    // setup console
                    HostSetup.SetupWindowsConsole(flow, "AdventureFramework Demo");

                    // begin
                    flow.Begin();
                }
            }
            catch (Exception e)
            {
                // display
                Console.WriteLine("Exception caught running demo: {0}", e.Message);

                // wait for exit
                Console.ReadKey();
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

        #region RegionConstruction

        #region Everglades

        /// <summary>
        /// Generate jungle Ben
        /// </summary>
        /// <returns>Jungle Ben</returns>
        public static PlayableCharacter generateJungleBen()
        {
            // create player
            PlayableCharacter player = new PlayableCharacter("Ben", "25 year old man", new Item("Knife", "A small pocket knife", true));

            // handle interaction
            player.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // if the knife
                if (i != null &&
                    i.Name.ToUpper() == "KNIFE")
                    // effect
                    return new InteractionResult(EInteractionEffect.FatalEffect, i, "You slash wildy at your own throat. You are dead");
                return new InteractionResult(EInteractionEffect.NoEffect, i);
            });

            // return
            return player;
        }

        /// <summary>
        /// Generate the Everglades region
        /// </summary>
        /// <param name="pC">The playable character that will appear in the overworld</param>
        /// <returns>Everglades</returns>
        private static Overworld generateEverglades(PlayableCharacter pC)
        {
            // generate region
            Region r = new Region("Everglades", "The starting place");

            // create rooms
            r.CreateRoom(new Room("Forest Entrance", new Description("You are standing on the edge of a beautiful forest. There is a parting in the trees to the north"), new Exit(ECardinalDirection.North)), 2, 0);
            r.CreateRoom(new Room("Forest Floor", new Description("The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest"), new Exit(ECardinalDirection.North), new Exit(ECardinalDirection.South)), 2, 1);
            r.CreateRoom(new Room("Cave Mouth", new Description("A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west"), new Exit(ECardinalDirection.North), new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.West)), 2, 2);

            // create shell item
            Item conchShell = new Item("Conch shell", "A pretty conch shell, it is about the size of a coconut", true);

            // setup conch shell interactions
            conchShell.Interaction = new InteractionCallback((Item item, IInteractWithItem target) =>
            {
                // select item by name
                switch (item.Name.ToUpper())
                {
                    case "KNIFE":
                        {
                            // return used up
                            return new InteractionResult(EInteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny peices. Without the conch shell you are well and truly fucked");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, item);
                        }
                }
            });

            // create rooms
            r.CreateRoom(new Room("Great Western Ocean", new Description("The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungerly. To the east is a small clearing"), new[] { new Exit(ECardinalDirection.East) }, conchShell), 1, 2);
            r.CreateRoom(new Room("Cave", new Description("The cave is so dark you struggling to see. A screetching noise is audible to the east"), new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.East)), 2, 3);

            // create inner cave
            Room innerCave = new Room("Inner Cave", string.Empty, new Exit(ECardinalDirection.West), new Exit(ECardinalDirection.North, true));

            // the interaction for the cave
            InteractionCallback innerCaveInteraction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // if the conch shell
                if (i != null &&
                    i.Name.ToUpper() == "CONCH SHELL")
                {
                    // unlock exits
                    innerCave[ECardinalDirection.North].Unlock();

                    // create effect
                    return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to peices");
                }

                if (i != null &&
                    i.Name.ToUpper() == "KNIFE")
                    // effect
                    return new InteractionResult(EInteractionEffect.NoEffect, i, "You slash wildy at the bats, but there are too many. Don't aggrevate them!");
                return new InteractionResult(EInteractionEffect.NoEffect, i);
            });

            // set interaction
            innerCave.Interaction = innerCaveInteraction;

            // specify conditional description of cave
            innerCave.SpecifyConditionalDescription(new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screetching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way", new Condition(() => { return !innerCave[ECardinalDirection.North].IsLocked; })));

            // add inner cave
            r.CreateRoom(innerCave, 3, 3);
            r.CreateRoom(new Room("Outskirts", new Description("A vast chasm falls away before you"), new Exit(ECardinalDirection.South)), 3, 4);

            // set start room
            r.SetStartRoom(0);

            // create overworld
            Overworld o = new Overworld("A place", "Who knows");

            // add region
            o.CreateRegion(r, 0, 0);

            // return overworld
            return o;
        }

        #endregion

        #region Flat

        /// <summary>
        /// Generate Ben
        /// </summary>
        /// <returns>Ben</returns>
        private static PlayableCharacter generateBen()
        {
            // create player
            PlayableCharacter player = new PlayableCharacter("Ben", "You are wearing shorts and a t-shirt");

            // handle interaction
            player.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select item
                switch (i.Name.ToUpper())
                {
                    case "EMPTY COFEE MUG":
                        {
                            // effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i, "If there was some coffee in the mug you could drink it");
                        }
                    case "GUITAR":
                        {
                            // effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");
                        }
                    default:
                        {
                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // return
            return player;
        }

        /// <summary>
        /// Generate the flag region
        /// </summary>
        /// <param name="pC">The playable character that will appear in the overworld</param>
        /// <returns>Flat</returns>
        private static Overworld generateFlat(PlayableCharacter pC)
        {
            // create flat
            Region flat = new Region("Flat 3 Bagley House", "Ben and Beth's Flat");

            // create bedroom
            Room bedroom = new Room("Bedroom",
                new Description("The bedroom is large, with one duck-egg blue wall. There is a double bed aginst the western wall, and a few other items of bedroom furniture are dotted around, but they all look pretty scruffy. To the north is a doorway leading to the hallway"),
                new[] { new Exit(ECardinalDirection.North) },
                new Item("Bed", "The bed is neatly made, Beth makes it every day. By your reckoning there are way too many cushions on it though...", false),
                new Item("Picture", "The picture is of some flowers and a mountian", false),
                new Item("TV", "The TV is small - the screen is only 14\"! Two DVD's are propped alongside it 'Miranda' and 'The Vicar Of Dibly'", false));


            // eastern hallway
            Room hallway1 = new Room("Eastern Hallway",
                new Description("The hallway is pretty narrow, and all the walls are bare except for a strange looking telephone. To the east is the front door, but it looks to heavy to open. To the south is the bedroom, to the west the hallway continues"),
                new[] { new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.East, true), new Exit(ECardinalDirection.West) },
                new Item("Telephone", "As soon as you pickup the telephone to examine it you hear hideous feedback. You replace it quickly!", false));


            // western hallway
            Room hallway2 = new Room("Western hallway",
                new Description("This hallway is a cotinuation of the Eastern Hallway, to the north is the Bathroom, to the west is the Kitchen, to the South is a neat looking Spare Room. The hallway continues to the East"),
                new Exit(ECardinalDirection.North), new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.East), new Exit(ECardinalDirection.West));


            // bath room
            Room bathroom = new Room("Bathroom",
                new Description("The bathroom is fairly small. There are some clothes drying on a clothes horse. A bath lies along the eastern wall. There is a remarkebly clean toilet and sink along the western wall, with a mirror above the sink. To the north is a large window, it is open and you can see out onto the roof of the flat below. The doorway to the south leads into the Western Hallway"),
                new[] { new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.North) },
                new Item("Bath", "A long but narrow bath. You wan't to fill it but you can't because there is a wetsuit in it.", false),
                new Item("Toilet", "A clean looking toilet. You lift the lid to take a look inside... ergh a floater! You flush the toilet but it just churns around! You close the lid and pretend it isn't there.", false),
                new Item("Mirror", "Looking in the mirror you see yourself clearly, and make a mental note to grow back some sideburns", false));

            // create mug
            Item mug = new Item("Empty Coffee Mug", "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange", true);

            // set kettle interaction
            mug.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // if works
                if (i != null)
                    // select item
                    switch (i.Name.ToUpper())
                    {
                        case "KETTLE":
                            {
                                // get item
                                Item item = target as Item;

                                // morph the coffee
                                item.Morph(new Item("Mug Of Coffee", "Hmm smells good, nice and bitter!", true));

                                // loose mug
                                return new InteractionResult(EInteractionEffect.ItemMorphed, i, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                            }
                        default:
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                throw new ArgumentException();
            });

            // bath room
            Room roof = new Room("Faustos Roof",
                string.Empty,
                new[] { new Exit(ECardinalDirection.South) },
                new Item("Sky light", "You peer down into the skylight, only to see a naked Italian man... cooking! Yikes! Not liking the idea of the accidents one could get into by cooking naked you look away quickly", false),
                mug);

            // set conditional description
            roof.Description = new ConditionalDescription("The roof is small and gravely, and it hurts your shoeless feet to stand on it. There is a large skylight in the center of the roof, and a empty coffee mug sits to the side, indicating someone has been here recently. The window behind you south leads back into the bathroom",
                "The roof is small and gravely, and it hurts your shoeless feet to stand on it. There is a large skylight in the center of the roof. The window behind you south leads back into the bathroom",
                new Condition(() => roof != null && roof.ContainsItem("Coffee Mug")));

            // create spare bedroom
            Room bedroom2 = new Room("Spare bedroom",
                string.Empty,
                new[] { new Exit(ECardinalDirection.North) },
                new Item("Gamecube", "A Nintendo Gamecube. You pop the disk cover, it looks like someone has been playing Killer7.", false),
                new Item("Guitar", "The guitar is blue, with birds inlaid on the fret board. On the headstock is someones name... 'Paul Reed Smith'. Who the hell is that. The guitar is litteraly begging to be played...", true));

            // set conditional description
            bedroom2.Description = new ConditionalDescription("You are in a very tidy room. The eastern wall is painted in a dark reddy colour. Against the south wall is a line of guitar amplifiers, all turned on. A very tidy blue guitar rests against the amps just begging to be played. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                "You are in a very tidy room. The eastern wall is painted in a dark reddy colour. Against the south wall is a line of guitar amplifiers, all turned on. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                new Condition(() => bedroom2 != null && bedroom2.ContainsItem("Guitar")));


            // create kettle
            Item kettle = new Item("Kettle", "The kettle has just boiled, you can tell because it is lightly steaming", true);

            // set kettle interaction
            kettle.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // get as item
                Item obj = target as Item;

                // if works
                if (obj != null)
                    // select item
                    switch (i.Name.ToUpper())
                    {
                        case "EMPTY COFFEE MUG":
                            {
                                // morph the coffee
                                i.Morph(new Item("Mug Of Coffee", "Hmm smells good, nice and bitter!", true));

                                // loose mug
                                return new InteractionResult(EInteractionEffect.ItemMorphed, i, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                            }
                        default:
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                throw new ArgumentException();
            });

            // the kitchen
            Room kitchen = new Room("Kitchen",
                new Description("The kitchen is a small area with work tops along the northern and eastern walls. There is a kettle on the work top, it has steam rising out of it's spout. There is a also window along the northern wall. Underneath the window is a hamster cage. To the south is the living room, the Western Hallway is to the east."),
                new[] { new Exit(ECardinalDirection.South), new Exit(ECardinalDirection.East) },
                new Item("Hamster Cage", "There is a pretty large hamster cage on the floor. When you go upto it you hear a small, but irritated sniffing. Mable sounds annoyed, best leave her alone for now.", false),
                kettle);


            // interaction
            kitchen.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // get as item
                Room obj = target as Room;

                // if works
                if (obj != null)
                    // select item
                    switch (i.Name.ToUpper())
                    {
                        case "GUITAR":
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i, "Playing guitar in the kitchen is pretty stupid don't you think?");
                            }
                        default:
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                throw new ArgumentException();
            });

            // create beth
            NonPlayableCharacter beth = new NonPlayableCharacter("Beth", "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV", new Conversation("Hello Ben", "You have a big dong"));

            // the lounge
            Room lounge = new Room("Lounge",
                string.Empty,
                new[] { new Exit(ECardinalDirection.North) },
                new[] { new Item("Map", "This things huge! Who would buy one of these? It looks pretty cheap, like it could have been bought from one of theose massive Swedish outlets. The resoultion of the map is too small to see your road on.", false), new Item("Canvas", "Wow, cool canvas. It is brightly painted with aliens and planets. On one planet there is a rabbit playing a guitar and whistling, but you can't see his face because he has his back turned to you. Something looks wrong with the rabbit...", false), new Item("Table", "The coffee table is one of those large oblong ones. It is made of reconstitued wood, made to look like birch", false), new Item("TV", "The TV is large, and is playing some program with a Chinease looking man dressing a half naked middle aged woman", false), new Item("Lead", "A 10m Venom instrument lead", true) },
                beth);

            // add conditional description
            lounge.Description = new ConditionalDescription("Your in a large stitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. There is what appears to be a lead of some sort poking out from underneath the sofa. The kitchen is to the north.",
                "Your in a large stitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. The kitchen is to the north.",
                new Condition(() => roof != null && roof.ContainsItem("Lead")));

            // handle interaction
            lounge.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // get as room
                Room obj = target as Room;

                // if works
                if (obj != null)
                    // select item
                    switch (i.Name.ToUpper())
                    {
                        case "MUG OF COFFEE":
                            {
                                // if beths in the lounge
                                if (obj.ContainsCharacter("Beth"))
                                    // result
                                    return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "Beth takes the cup of coffee and smiles. Brownie points to you!");

                                // morph mug back
                                i.Morph(new Item("Empty Coffee Mug", "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange.", true));

                                // result
                                return new InteractionResult(EInteractionEffect.ItemMorphed, i, "As no one is about you decide to drink the coffee yourself. Your nose wasn't lying, it is bitter but delicious.");
                            }
                        case "EMPTY COFEE MUG":
                            {
                                // add cup to lounge
                                obj.AddItem(i);

                                // result
                                return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "You put the mug down on the coffee table, sick of carrying the bloody thing around. Beth is none too impressed.");
                            }
                        case "GUITAR":
                            {
                                // result
                                return new InteractionResult(EInteractionEffect.NoEffect, i, "You strum the guitar frantically trying to impress Beth, she smiles but looks at you like you are a little mental. The guitar just isn't loud enough when it is not plugged in...");
                            }
                        default:
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                throw new ArgumentException();
            });

            // interaction
            bedroom2.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // get as room
                Room obj = target as Room;

                // if works
                if (obj != null)
                    // select item
                    switch (i.Name.ToUpper())
                    {
                        case "LEAD":
                            {
                                // add item to room as static
                                obj.AddItem(new Item(i.Name, i.Description.GetDescription(), true));

                                // return result
                                return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "The lead fits snugly into the input socket on the amp");
                            }
                        case "GUITAR":
                            {
                                // if a lead
                                if (obj.ContainsItem("LEAD"))
                                {
                                    // unlock door
                                    hallway1[ECardinalDirection.East].Unlock();

                                    // remove beth
                                    lounge.RemoveCharacterFromRoom("Beth");

                                    // play guitar
                                    return new InteractionResult(EInteractionEffect.NoEffect, i, "The guitar plugs in with a satisfying click. You play some punk and the amp sings. Beths had enough! She bolts for the front door leaving it wide open! You are free to leave the flat! You unplug the guitar");
                                }

                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i, "You have no lead so you can't use the guitar with the amp...");
                            }
                        default:
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                throw new ArgumentException();
            });

            // create all rooms
            flat.CreateRoom(bedroom, 2, 0);
            flat.CreateRoom(hallway1, 2, 1);
            flat.CreateRoom(hallway2, 1, 1);
            flat.CreateRoom(bathroom, 1, 2);
            flat.CreateRoom(roof, 1, 3);
            flat.CreateRoom(bedroom2, 1, 0);
            flat.CreateRoom(kitchen, 0, 1);
            flat.CreateRoom(lounge, 0, 0);
            flat.CreateRoom(new Room("Stairway", new Description("You are in the Stairway. It is dimly lit because the bulbs have blown again, to the north is a staircase leading down to the other flats. Fausto, your next door neighbour is standing naked at the bottom of the stairs. He looks pretty pissed off about all the noise, and doesn't look like he is going to let you past. To the west is the front door of the flat."), new Exit(ECardinalDirection.West)), 3, 1);

            // set start room to bedroom
            flat.SetStartRoom(bedroom);

            // create overworld
            Overworld flatWorld = new Overworld("Minehead", "A sleepy town");

            // create region
            flatWorld.CreateRegion(flat, 0, 0);

            // return overworld
            return flatWorld;
        }

        #endregion

        #region Asteroids

        /// <summary>
        /// Generate an astronaut
        /// </summary>
        /// <returns>The astronaut</returns>
        private static PlayableCharacter generateAstronaut()
        {
            // create player
            PlayableCharacter p = new PlayableCharacter("Captain Scott", new Description("They used to call you Captain Scott, you are an astronaut"), new Item("Ray Gun", new Description("A handheld pistol made from alluminium. It is weighty to hold and feels you with confidence."), true));

            // set interaction
            p.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "RAY GUN":
                        {
                            // interaction
                            return new InteractionResult(EInteractionEffect.NoEffect, i, "Blaowh Blaowh! You fire the ray gun, and you briefly see the ray in the atmosphere. It acts like a prism, and for a brief second the colours look beautiful");
                        }
                    default:
                        {
                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // return player
            return p;
        }

        /// <summary>
        /// Generate a new world
        /// </summary>
        /// <param name="pC">The playable character that will appear in the overworld</param>
        /// <returns>A new world</returns>
        private static Overworld generateNewWorld(PlayableCharacter pC)
        {
            // create over world
            Overworld o = new Overworld("Asteroid", "A lonely asteroid");

            // create region
            Region asteroid = new Region("Asteroid", new Description("You are on an astroid. Your sensors say the atmosphere is extreamly hostile."));

            // create region
            Region home = new Region("Home", new Description("You are back home, phew!"));

            // create bedroom
            home.CreateRoom(new Room("Bedroom", new Description("You are in your bedroom. Phew, what an adventure")), 0, 0);

            // create crash site
            Room crashSite = new Room("Crash Site", new Description("This is the crash site. Your ship lies is in smouldering peices in front of you. The ground appears to be rocky, but not earthly. There is no sign of life in any direction. The asteroid is so small that any movement results in you being back where you are"));

            // ship
            Item ship = new Item("Ruined Ship", new Description("The ship lies in ruins"), false);

            // set interaction
            ship.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "RAY GUN":
                        {
                            // get as item
                            Item obj = target as Item;

                            // if works
                            if (obj != null)
                            {
                                // morph
                                obj.Morph(new Item("Shrapnel", "A jagged peice of shrapnell", true));

                                // interaction
                                return new InteractionResult(EInteractionEffect.ItemMorphed, i, "Blaowh Blaowh! You fire the ray gun, and your ray gun at the fuel tank, and the ship explodes. Shrapnel hits your entire body, but luckily you suit remains intact. A jagged piece of shrapnel lies on the asteroids surface");
                            }

                            // throw exception
                            throw new ArgumentException();
                        }
                    default:
                        {
                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // set examination
            ship.Examination = new ExaminationCallback((IExaminable obj) =>
            {
                // select name
                switch (obj.Name.ToUpper())
                {
                    case "SHRAPNEL":
                        {
                            // get as item
                            Item i = obj as Item;

                            // if works
                            if (i != null)
                            {
                                // morph
                                i.Morph(new Item("Flint", "A piece of flint", true));

                                // standard
                                return new ExaminationResult("On closer inspection it is a piece of flint");
                            }

                            // throw exception
                            throw new ArgumentException();
                        }
                    case "FLINT":
                        {
                            // get as item
                            Item i = obj as Item;

                            // if works
                            if (i != null)
                            {
                                // if on asteroid
                                if (o.CurrentRegion == asteroid)
                                    // move
                                    o.MoveRegion(home);
                                else
                                    // move
                                    o.MoveRegion(asteroid);

                                // standard
                                return new ExaminationResult("The flint is magic flint. You are teleported");
                            }

                            // throw exception
                            throw new ArgumentException();
                        }
                    default:
                        {
                            // standard
                            return new ExaminationResult(obj.Description.GetDescription());
                        }
                }
            });

            // add ship
            crashSite.AddItem(ship);

            // create room
            asteroid.CreateRoom(crashSite, 0, 0);

            // create region
            o.CreateRegion(asteroid, 0, 0);

            // create region
            o.CreateRegion(home, 0, 1);

            // return
            return o;
        }

        #endregion

        #region Zelda

        /// <summary>
        /// Generate Link
        /// </summary>
        /// <returns>Link</returns>
        public static PlayableCharacter generateLink()
        {
            // create character
            PlayableCharacter link = new PlayableCharacter("Link", "You are a Kokiri, dressed in the traditional green Kokiri garments");

            // set examination
            link.Examination = new ExaminationCallback((IExaminable t) =>
            {
                // create frame
                //ASCIIImageFrame frame = ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["link"], Console.WindowWidth, Console.WindowHeight);

                // create animation
                ASCIIAnimationFrame frame = new ASCIIAnimationFrame(Timeout.Infinite, 125, false, ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario1"], Console.WindowWidth, Console.WindowHeight, -20),
                    ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario2"], Console.WindowWidth, Console.WindowHeight, -20),
                    ASCIIImageFrame.Create(InGameGraphics.UserDefinedGraphics["Mario3"], Console.WindowWidth, Console.WindowHeight, -20));

                // display frame
                FrameDrawer.DisplaySpecialFrame(frame);

                // return result
                return new ExaminationResult("Self", EExaminationResults.SelfContained);
            });

            // create sword
            Item sword = new Item("Sword", "A small, short arm sword, handed down from generation to generation of Kokiri. The bladed isn't that sharp but it will do for now", true);

            // add sword
            link.AquireItem(sword);

            // add shield
            link.AquireItem(new Item("Deku Shield", "A small wooden shield. It is made from the wood of the Great Deku Tree. There are scorch marks on the surface", true));

            // add ocarina
            link.AquireItem(new Item("Ocarina", "A small instrument made of clay. It sounds a bit like a flute, but it isn't easy to play", true));

            // add bombs
            link.AquireItem(new Item("Bombs", "You have a bomb bag stuffed full of bombs. The bombs are small, black and round and have a short fuse on the top", true));

            // set interction
            link.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select item
                switch (i.Name.ToUpper())
                {
                    case "OCARINA":
                        {
                            // return result
                            return new InteractionResult(EInteractionEffect.NoEffect, i, "You raise the ocarina to your lips and play the song that guy in the windmill taught you. Nothing happens, but you feel better anyway");
                        }
                    default:
                        {
                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // return
            return link;
        }

        /// <summary>
        /// Generate Hyrule
        /// </summary>
        /// <param name="pC">The playable character that will appear in the overworld</param>
        /// <returns>Hyrule</returns>
        public static Overworld generateHyrule(PlayableCharacter pC)
        {
            // create hyrule
            Overworld hyrule = new Overworld("Hyrule", "The land of destiny");

            // create castle grounds
            Region castleGrounds = new Region("Castle Grounds", "The gardens around Hyrule are beautiful and well kept");

            // create gate
            Room gate = new Room("Gateway to Hyrule Castle Grounds", string.Empty, new Exit(ECardinalDirection.North, true));

            // create guard
            NonPlayableCharacter guard = new NonPlayableCharacter("Guard", "The guard looks mean, with a face not even a mother could love. He also looks decidely shifty, considering he is a castle guard");

            // set description
            gate.Description = new ConditionalDescription("A large stone archway looms infront of you. Blocking the arch is a strong iron gate. It looks like is locked tight. There is a soilder guarding the gate. To the East the cliff face is covered in vines", "A large stone archway looms infront of you. To the East the cliff face is covered in vines", new Condition(() => { return guard.IsAlive; }));

            // add conversation
            guard.Conversation = new Conversation("Woah there elf boy, where do you think you are going?");

            // create keys
            Item keys = new Item("Keys", "A set of keys on a large key ring. One key is particulay big and looks as though it fits the gate...", true);

            // not visible
            keys.IsPlayerVisible = false;

            // do interaction
            guard.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // check guard is alive
                if (guard.IsAlive)
                    // select name
                    switch (i.Name.ToUpper())
                    {
                        case "BOW":
                            {
                                // kill guard
                                guard.Kill();

                                // keys visible
                                keys.IsPlayerVisible = true;

                                // no effect
                                return new InteractionResult(EInteractionEffect.SelfContained, i, "You draw the bow and fire an arrow directly through the soilders head. He drops to the floor, spilling his keys");
                            }
                        case "DEKU SHIELD":
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i, "Guard: \"Where'd you go big boy?\"");
                            }
                        case "SWORD":
                            {
                                // die
                                return new InteractionResult(EInteractionEffect.FatalEffect, i, "You lunge at the guard, he dodges your slash, and strikes back. His sword punctures your tunic, and lacerates your heart. The world turns black");
                            }
                        case "OCARINA":
                            {
                                // no effect
                                return new InteractionResult(EInteractionEffect.NoEffect, i, "You play the ocarina, it sings true, but the guard is unmoved by it");
                            }
                        default:
                            {
                                return new InteractionResult(EInteractionEffect.NoEffect, i);
                            }
                    }

                return new InteractionResult(EInteractionEffect.NoEffect, i, "The guard is dead!");
            });

            // add guard
            gate.AddCharacter(guard);

            // add keys
            gate.AddItem(keys);

            // create rear of gate
            Room rearOfGate = new Room("Rear Of Gate", string.Empty, new Exit(ECardinalDirection.North), new Exit(ECardinalDirection.East), new Exit(ECardinalDirection.South, true));

            // set description
            rearOfGate.Description = new ConditionalDescription("You are on the otherside of the castle's gate! You can see the guard with his back to you, but he has no idea you are here. He must have though you wen't back to Hyrule town. To the North the track to the castle continues, to the East a path climbs onto the cliff top", "You are on the otherside of the castle's gate. To the North the track to the castle continues, to the East a path climbs onto the cliff top", new Condition(() => { return guard.IsAlive; }));

            // set interaction
            gate.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "KEYS":
                        {
                            // unlock door pair
                            castleGrounds.UnlockDoorPair(ECardinalDirection.North);

                            // no effect
                            return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "The key fits the lock! You turn it and the gate clanks open!");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // set interaction
            rearOfGate.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "KEYS":
                        {
                            // unlock door pair
                            castleGrounds.UnlockDoorPair(ECardinalDirection.South);

                            // no effect
                            return new InteractionResult(EInteractionEffect.ItemUsedUp, i, "The key fits the lock! You turn it and the gate clanks open!");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // create cliff top
            Room cliffTop = new Room("Cliff Top", new Description("The cliff overlooks the gate. You can see the top of the guards head form here, but he is unaware of your presence. To the North the clifftop continues"), new Exit(ECardinalDirection.North));

            // create vines
            Item vines = new Item("Vines", "The vines grow all the way to the top of the cliff face", false);

            // add additional command
            vines.AdditionalCommands.Add(new ActionableCommand("Climb", "Climb the vines", true, new ActionCallback(() =>
            {
                // if on gate
                if (castleGrounds.CurrentRoom == gate)
                {
                    // move to cliff top
                    castleGrounds.Move(cliffTop.Name);

                    // return 
                    return new InteractionResult(EInteractionEffect.NoEffect, vines, "Tugging at the vines they appear pretty sturdy. You climb up and true enough they hold!");
                }

                // move to gate
                castleGrounds.Move(gate.Name);

                // return 
                return new InteractionResult(EInteractionEffect.NoEffect, vines, "Tugging at the vines they appear pretty sturdy. You lower yourself down and true enough they hold!");
            })));

            // handle interaction
            vines.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "SWORD":
                        {
                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i, "You slash at the vines. A few leaves fall off. Never the fan of gardening you decide to let nature take it's course");
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // add vines
            gate.AddItem(vines);

            // add vines
            cliffTop.AddItem(vines);

            // create northern cliff top
            Room cliffTopNorth = new Room("Cliftop North", "The clifftop descends to the West where it meets the otherside of the gate, to the South it contiues along the cliff top", new Exit(ECardinalDirection.West), new Exit(ECardinalDirection.South));

            // create corner
            Room cornerInTrack = new Room("Track Corner", "The track to the castle is laid in a herringbone pattern, it looks very expensive! The track rounds a sharp western corner, where it continues. There is a wooden Signpost on the corner of the apex. Behind the signpost is a suspicious looking pile of rocks. To the South you can see the castle gate", new Exit(ECardinalDirection.North), new Exit(ECardinalDirection.South));

            // create signpost
            Item signPost = new Item("Signpost", "The sign post is old and made of wood. It Says 'Great Fairy's Fountain'", false);

            // set interaction
            signPost.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "SWORD":
                        {
                            // if target is item
                            if (target is Item)
                            {
                                // get post
                                Item s = target as Item;

                                // morph
                                s.Morph(new Item("Ruined Signpost", "The signpost lies in ruins. You can't make out what it says because you trashed it", false));

                                // no effect
                                return new InteractionResult(EInteractionEffect.ItemMorphed, i, "You slash at the signpost. It splits in two places and falls to the ground");
                            }

                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // add signpost
            cornerInTrack.AddItem(signPost);

            // not visible
            cornerInTrack[ECardinalDirection.North].IsPlayerVisible = false;

            // create pile of rocks
            Item pileOfRocks = new Item("Pile of rocks", "There is a very suspicious pile of rocks against the nothern side of the cliff. They don't look like they have crumbled off in a landslide because they are placed too neatly", false);

            // set interaction
            pileOfRocks.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "BOMBS":
                        {
                            // if target is item
                            if (target is Item)
                            {
                                // make visible
                                cornerInTrack[ECardinalDirection.North].IsPlayerVisible = true;

                                // no effect
                                return new InteractionResult(EInteractionEffect.TargetUsedUp, i, "You place a bomb by the pile of rocks and light the fuse, then run to a safe distance. You wait as the fuse slowly burns, and then BOOOM! the bomb explodes! Pieces of rock are thrown everywhere. When the dust settles a cave mouth is revealed where the pile of rocks was");
                            }

                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // add rocks
            cornerInTrack.AddItem(pileOfRocks);

            // create cave
            Room greatFairyCave = new Room("Great Fairy Cave", "The cave is beautiful! It is covered in glow-worms and there is a large marble pool in the middle. The pool is filled with the purest water you have ever seen. On the floor infront of the pool is engraved the Crest of the Royal Family Of Hyrule. To the South light floods in through the cave mouth", new Exit(ECardinalDirection.South));

            // create crest
            Item crest = new Item("Crest", new Description("A crest of the Royal Family Of Hyrule is engraved on the floor, embossed in gold filagree"), false);

            // creaye inhabitant
            NonPlayableCharacter greatFairy = new NonPlayableCharacter("Great Fairy", "The Great Fairy is beautiful! She has magennat hair, and is dressed in a bekini made of ivy");

            // add new converstaion
            greatFairy.Conversation = new Conversation(new ConversationElement("Hello Link, thank you for destroying the rocks blocking the entrance to my cave. As a thank you please take this bow", new Action(() => { greatFairyCave.AddItem(new Item("Bow", "A wooden bow, this was a present from the Great Fairy", true)); })));

            // not visible
            greatFairy.IsPlayerVisible = false;

            // add fairy
            greatFairyCave.AddCharacter(greatFairy);

            // create interaction
            crest.Interaction = new InteractionCallback((Item i, IInteractWithItem target) =>
            {
                // select name
                switch (i.Name.ToUpper())
                {
                    case "OCARINA":
                        {
                            // if no fairy
                            if (!greatFairy.IsPlayerVisible)
                            {
                                // make visible
                                greatFairy.IsPlayerVisible = true;

                                // self contained
                                return new InteractionResult(EInteractionEffect.SelfContained, i, "You play Zelda's lullaby, it echos around the cavern. The water ripples slightly from the center outwards, and they continue to build up. A fairy drabbed in ivy rises from the pool");
                            }

                            // no effect
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                    default:
                        {
                            return new InteractionResult(EInteractionEffect.NoEffect, i);
                        }
                }
            });

            // add crest
            greatFairyCave.AddItem(crest);

            // create rooms
            castleGrounds.CreateRoom(gate, 2, 0);
            castleGrounds.CreateRoom(cliffTop, 3, 0);
            castleGrounds.CreateRoom(cliffTopNorth, 3, 1);
            castleGrounds.CreateRoom(rearOfGate, 2, 1);
            castleGrounds.CreateRoom(cornerInTrack, 2, 2);
            castleGrounds.CreateRoom(greatFairyCave, 2, 3);

            // create region
            hyrule.CreateRegion(castleGrounds, 0, 0);

            // return hyrule
            return hyrule;
        }

        #endregion

        #endregion

        #endregion
    }
}