using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Examples.Assets
{
    internal static class Everglades
    {
        private const string Knife = "Knife";
        private const string ConchShell = "Conch Shell";
        private const string Region = "Everglades";
        private const string ForestEntrance = "Forest Entrance";
        private const string ForestFloor = "Forest Floor";
        private const string CaveMouth = "Cave Mouth";
        private const string Cave = "Cave";
        private const string InnerCave = "Inner Cave";
        private const string GreatWesternOcean = "Great Western Ocean";
        private const string Outskirts = "Outskirts";
        private const string Overworld = "Unknown Place";

        internal static PlayableCharacter GeneratePC()
        {
            var player = new PlayableCharacter("Ben".ToIdentifier(), "25 year old man".ToDescription(), new Item(Knife.ToIdentifier(), "A small pocket knife".ToDescription(), true))
            {
                Interaction = (i, target) =>
                {
                    if (i != null && Knife.EqualsExaminable(i))
                        return new InteractionResult(InteractionEffect.FatalEffect, i, "You slash wildly at your own throat. You are dead");

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }
            };

            return player;
        }

        internal static Overworld GenerateOverworld(PlayableCharacter pC)
        {
            var r = new Region(Region.ToIdentifier(), "The starting place".ToDescription());
            r.AddRoom(new Room(ForestEntrance.ToIdentifier(), new Description("You are standing on the edge of a beautiful forest. There is a parting in the trees to the north"), new Exit(CardinalDirection.North)), 2, 0);
            r.AddRoom(new Room(ForestFloor.ToIdentifier(), new Description("The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest"), new Exit(CardinalDirection.North), new Exit(CardinalDirection.South)), 2, 1);
            r.AddRoom(new Room(CaveMouth.ToIdentifier(), new Description("A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west"), new Exit(CardinalDirection.North), new Exit(CardinalDirection.South), new Exit(CardinalDirection.West)), 2, 2);

            var conchShell = new Item(ConchShell.ToIdentifier(), "A pretty conch shell, it is about the size of a coconut".ToDescription(), true)
            {
                Interaction = (item, target) =>
                {
                    switch (item.Identifier.IdentifiableName)
                    {
                        case Knife:
                            return new InteractionResult(InteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny pieces. Without the conch shell you are well and truly fucked");
                        default:
                            return new InteractionResult(InteractionEffect.NoEffect, item);
                    }
                }
            };

            r.AddRoom(new Room(GreatWesternOcean.ToIdentifier(), new Description("The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungrily. To the east is a small clearing"), new[] { new Exit(CardinalDirection.East) }, conchShell), 1, 2);
            r.AddRoom(new Room(Cave.ToIdentifier(), new Description("The cave is so dark you struggling to see. A screeching noise is audible to the east"), new Exit(CardinalDirection.South), new Exit(CardinalDirection.East)), 2, 3);

            var innerCave = new Room(InnerCave.ToIdentifier(), new Description(string.Empty), new Exit(CardinalDirection.West), new Exit(CardinalDirection.North, true));

            InteractionCallback innerCaveInteraction = (i, target) =>
            {
                if (i != null && ConchShell.EqualsExaminable(i))
                {
                    innerCave[CardinalDirection.North].Unlock();
                    return new InteractionResult(InteractionEffect.ItemUsedUp, i, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces");
                }

                if (i != null && Knife.EqualsExaminable(i))
                    return new InteractionResult(InteractionEffect.NoEffect, i, "You slash wildly at the bats, but there are too many. Don't aggravate them!");

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            innerCave.Interaction = innerCaveInteraction;
            innerCave.SpecifyConditionalDescription(new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way", () => !innerCave[CardinalDirection.North].IsLocked));
            r.AddRoom(innerCave, 3, 3);
            r.AddRoom(new Room(Outskirts.ToIdentifier(), new Description("A vast chasm falls away before you"), new Exit(CardinalDirection.South)), 3, 4);
            r.SetStartRoom(0);

            var o = new Overworld(Overworld.ToIdentifier(), "An unknown world".ToDescription());
            o.Regions.Add(r);
            return o;
        }
    }
}
