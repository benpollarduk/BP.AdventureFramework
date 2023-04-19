using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utils;

namespace BP.AdventureFramework.Examples.Assets
{
    internal static class Flat
    {
        private const string Ben = "Ben";
        private const string EmptyCoffeeMug = "Empty Coffee Mug";
        private const string MugOfCoffee = "Mug Of Coffee";
        private const string Guitar = "Guitar";
        private const string Flat3 = "Flat";
        private const string Bed = "Bed";
        private const string TV = "TV";
        private const string Bedroom = "Bedroom";
        private const string Picture = "Picture";
        private const string EasternHallway = "Eastern Hallway";
        private const string WesternHallway = "Western Hallway";
        private const string Telephone = "Telephone";
        private const string Bathroom = "Bathroom";
        private const string Bath = "Bath";
        private const string Toilet = "Toilet";
        private const string Kettle = "Kettle";
        private const string Mirror = "Mirror";
        private const string Lead = "Lead";
        private const string FaustosRoof = "Faustos Roof";
        private const string Skylight = "Sky Light";
        private const string SpareBedroom = "Spare Bedroom";
        private const string Kitchen = "Kitchen";
        private const string Gamecube = "Gamecube";
        private const string HamsterCage = "Hamster Cage";
        private const string Map = "Map";
        private const string Table = "Table";
        private const string Canvas = "Canvas";
        private const string Lounge = "Lounge";
        private const string Beth = "Beth";
        private const string Stairway = "Stairway";
        private const string Smalltown = "Smalltown";

        internal static PlayableCharacter GeneratePC()
        {
            var player = new PlayableCharacter(Ben, "You are wearing shorts and a t-shirt")
            {
                Interaction = (i, target) =>
                {
                    if (EmptyCoffeeMug.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "If there was some coffee in the mug you could drink it");

                    if (Guitar.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "You bust out some Bad Religion. Cracking, shame the guitar isn't plugged in to an amplified though...");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        internal static Overworld GenerateOverworld(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Flat3, "Ben and Beth's Flat");

            var bedroom = new Room(Bedroom,
                "The bedroom is large, with one duck-egg blue wall. There is a double bed against the western wall, and a few other items of bedroom furniture are dotted around, but they all look pretty scruffy. To the north is a doorway leading to the hallway",
                new[] { new Exit(CardinalDirection.North) },
                new Item(Bed, "The bed is neatly made, Beth makes it every day. By your reckoning there are way too many cushions on it though..."),
                new Item(Picture, "The picture is of some flowers and a mountain"),
                new Item(TV, "The TV is small - the screen is only 14\"! Two DVD's are propped alongside it 'Miranda' and 'The Vicar Of Dibly'"));

            var hallway1 = new Room(EasternHallway,
                "The hallway is pretty narrow, and all the walls are bare except for a strange looking telephone. To the east is the front door, but it looks to heavy to open. To the south is the bedroom, to the west the hallway continues",
                new[] { new Exit(CardinalDirection.South), new Exit(CardinalDirection.East, true), new Exit(CardinalDirection.West) },
                new Item(Telephone, "As soon as you pickup the telephone to examine it you hear hideous feedback. You replace it quickly!"));

            var hallway2 = new Room(WesternHallway,
                "This hallway is a continuation of the Eastern Hallway, to the north is the Bathroom, to the west is the Kitchen, to the South is a neat looking Spare Room. The hallway continues to the East",
                new Exit(CardinalDirection.North), new Exit(CardinalDirection.South), new Exit(CardinalDirection.East), new Exit(CardinalDirection.West));

            var bathroom = new Room(Bathroom,
                "The bathroom is fairly small. There are some clothes drying on a clothes horse. A bath lies along the eastern wall. There is a remarkably clean toilet and sink along the western wall, with a mirror above the sink. To the north is a large window, it is open and you can see out onto the roof of the flat below. The doorway to the south leads into the Western Hallway",
                new[] { new Exit(CardinalDirection.South), new Exit(CardinalDirection.North) },
                new Item(Bath, "A long but narrow bath. You wan't to fill it but you can't because there is a wetsuit in it."),
                new Item(Toilet, "A clean looking toilet. You lift the lid to take a look inside... ergh a floater! You flush the toilet but it just churns around! You close the lid and pretend it isn't there."),
                new Item(Mirror, "Looking in the mirror you see yourself clearly, and make a mental note to grow back some sideburns"));

            var mug = new Item(EmptyCoffeeMug, "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange", true)
            {
                Interaction = (i, target) =>
                {
                    if (Kettle.EqualsIdentifier(i.Identifier))
                    {
                        var item = target as Item;
                        item?.Morph(new Item(MugOfCoffee, "Hmmm smells good, nice and bitter!", true));
                        return new InteractionResult(InteractionEffect.ItemMorphed, i, "You put some instant coffee graduals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            var roof = new Room(FaustosRoof,
                string.Empty,
                new[] { new Exit(CardinalDirection.South) },
                new Item(Skylight, "You peer down into the skylight, only to see a naked Italian man... cooking! Yikes! Not liking the idea of the accidents one could get into by cooking naked you look away quickly"),
                mug);

            roof.Description = new ConditionalDescription("The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof, and a empty coffee mug sits to the side, indicating someone has been here recently. The window behind you south leads back into the bathroom",
                "The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof. The window behind you south leads back into the bathroom",
                () => roof.ContainsItem(MugOfCoffee));

            var bedroom2 = new Room(SpareBedroom,
                string.Empty,
                new[] { new Exit(CardinalDirection.North) },
                new Item(Gamecube, "A Nintendo Gamecube. You pop the disk cover, it looks like someone has been playing Killer7."),
                new Item(Guitar, "The guitar is blue, with birds inlaid on the fret board. On the headstock is someones name... 'Paul Reed Smith'. Who the hell is that. The guitar is literally begging to be played...", true));

            bedroom2.Description = new ConditionalDescription("You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. A very tidy blue guitar rests against the amps just begging to be played. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                "You are in a very tidy room. The eastern wall is painted in a dark red colour. Against the south wall is a line of guitar amplifiers, all turned on. There is a Gamecube against the northern wall. A doorway to the north leads back to the Western Hallway.",
                () => bedroom2.ContainsItem(Guitar));

            var kettle = new Item("Kettle", "The kettle has just boiled, you can tell because it is lightly steaming", true)
            {
                Interaction = (i, target) =>
                {
                    var obj = target as Item;

                    if (obj != null)
                    {
                        if (EmptyCoffeeMug.EqualsIdentifier(i.Identifier))
                        {
                            i.Morph(new Item(MugOfCoffee, "Hmm smells good, nice and bitter!", true));
                            return new InteractionResult(InteractionEffect.ItemMorphed, i, "You put some instant coffee granuals into the mug and add some freshly boiled water from the Kettle. The coffee smells amazing!");
                        }
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            var kitchen = new Room(Kitchen,
                "The kitchen is a small area with work tops along the northern and eastern walls. There is a kettle on the work top, it has steam rising out of it's spout. There is a also window along the northern wall. Underneath the window is a hamster cage. To the south is the living room, the Western Hallway is to the east.",
                new[] { new Exit(CardinalDirection.South), new Exit(CardinalDirection.East) },
                new Item(HamsterCage, "There is a pretty large hamster cage on the floor. When you go up to it you hear a small, but irritated sniffing. Mable sounds annoyed, best leave her alone for now."),
                kettle)
            {
                Interaction = (i, target) =>
                {
                    var obj = target as Room;

                    if (obj != null)
                    {
                        if (Guitar.EqualsIdentifier(i.Identifier))
                            return new InteractionResult(InteractionEffect.NoEffect, i, "Playing guitar in the kitchen is pretty stupid don't you think?");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            var beth = new NonPlayableCharacter(Beth, "Beth is very pretty, she has blue eyes and ginger hair. She is sat on the sofa watching the TV", new Conversation(new ConversationElement("Hello Ben"), new ConversationElement("How are you?")));

            var lounge = new Room(Lounge,
                string.Empty,
                new[] { new Exit(CardinalDirection.North) },
                new Item(Map, "This things huge! Who would buy one of these? It looks pretty cheap, like it could have been bought from one of those massive Swedish outlets. The resolution of the map is too small to see your road on."),
                new Item(Canvas, "Wow, cool canvas. It is brightly painted with aliens and planets. On one planet there is a rabbit playing a guitar and whistling, but you can't see his face because he has his back turned to you. Something looks wrong with the rabbit..."),
                new Item(Table, "The coffee table is one of those large oblong ones. It is made of reconstituted wood, made to look like birch"),
                new Item(TV, "The TV is large, and is playing some program with a Chinese looking man dressing a half naked middle aged woman"),
                new Item(Lead, "A 10m Venom instrument lead", true));

            lounge.AddCharacter(beth);

            lounge.Description = new ConditionalDescription("Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. There is what appears to be a lead of some sort poking out from underneath the sofa. The kitchen is to the north.",
                "Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. The kitchen is to the north.",
                () => roof.ContainsItem(Lead));

            lounge.Interaction = (i, target) =>
            {
                var obj = target as Room;

                if (obj != null)
                {
                    if (MugOfCoffee.EqualsIdentifier(i.Identifier))
                    {
                        if (obj.ContainsCharacter(Beth))
                            return new InteractionResult(InteractionEffect.ItemUsedUp, i, "Beth takes the cup of coffee and smiles. Brownie points to you!");

                        i.Morph(new Item(EmptyCoffeeMug, "A coffee mug. It has an ugly hand painted picture of a man with green hair and enormous sideburns painted on the side of it. Underneath it says 'The Sideburn Monster Rides again'. Strange.", true));
                        return new InteractionResult(InteractionEffect.ItemMorphed, i, "As no one is about you decide to drink the coffee yourself. Your nose wasn't lying, it is bitter but delicious.");

                    }

                    if (EmptyCoffeeMug.EqualsIdentifier(i.Identifier))
                    {
                        obj.AddItem(i);
                        return new InteractionResult(InteractionEffect.ItemUsedUp, i, "You put the mug down on the coffee table, sick of carrying the bloody thing around. Beth is none too impressed.");
                    }

                    if (Guitar.EqualsIdentifier(i.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, i, "You strum the guitar frantically trying to impress Beth, she smiles but looks at you like you are a fool. The guitar just isn't loud enough when it is not plugged in...");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            // interaction
            bedroom2.Interaction = (i, target) =>
            {
                var obj = target as Room;

                if (obj != null)
                {
                    if (Lead.EqualsIdentifier(i.Identifier))
                    {
                        obj.AddItem(new Item(i.Identifier, i.Description, true));
                        return new InteractionResult(InteractionEffect.ItemUsedUp, i, "The lead fits snugly into the input socket on the amp");
                    }

                    if (Guitar.EqualsIdentifier(i.Identifier))
                    {
                        if (obj.ContainsItem(Lead))
                        {
                            hallway1[CardinalDirection.East].Unlock();

                            if (lounge.FindCharacter(Beth, out var b))
                            {
                                lounge.RemoveCharacter(b);
                                return new InteractionResult(InteractionEffect.NoEffect, i, "The guitar plugs in with a satisfying click. You play some punk and the amp sings. Beth's had enough! She bolts for the front door leaving it wide open! You are free to leave the flat! You unplug the guitar");
                            }

                            return new InteractionResult(InteractionEffect.NoEffect, i, "The guitar plugs in with a satisfying click. You play some punk and the amp sings");
                        }

                        return new InteractionResult(InteractionEffect.NoEffect, i, "You have no lead so you can't use the guitar with the amp...");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            var stairway = new Room(Stairway, "You are in the Stairway. It is dimly lit because the bulbs have blown again, to the north is a staircase leading down to the other flats. Fausto, your next door neighbour is standing naked at the bottom of the stairs. He looks pretty pissed off about all the noise, and doesn't look like he is going to let you past. To the west is the front door of the flat.", new Exit(CardinalDirection.West));

            // create all rooms
            regionMaker[2, 0] = bedroom;
            regionMaker[2, 1] = hallway1;
            regionMaker[1, 1] = hallway2;
            regionMaker[1, 2] = bathroom;
            regionMaker[1, 3] = roof;
            regionMaker[1, 0] = bedroom2;
            regionMaker[0, 1] = kitchen;
            regionMaker[0, 0] = lounge;
            regionMaker[3, 1] = stairway;

            return new OverworldMaker(Smalltown, "A sleepy town", regionMaker).Make();
        }
    }
}
