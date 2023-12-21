using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.NPCs;
using BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat
{
    internal class Flat : RegionTemplate<Flat>
    {
        #region Constants

        private const string Name = "Flat";
        private const string Description = "Ben and Beth's Flat";

        #endregion

        #region Overrides of RegionTemplate<Flat>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var roof = Roof.Create();
            var easternHallway = EasternHallway.Create();
            var spareBedroom = SpareBedroom.Create();
            var lounge = Lounge.Create();

            lounge.Description = new ConditionalDescription("Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. There is what appears to be a lead of some sort poking out from underneath the sofa. The kitchen is to the north.",
                "Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. The kitchen is to the north.",
                () => roof.ContainsItem(Lead.Name));

            spareBedroom.Interaction = (i, target) =>
            {
                var obj = target as Room;

                if (obj != null)
                {
                    if (Lead.Name.EqualsIdentifier(i.Identifier))
                    {
                        obj.AddItem(new Item(i.Identifier, i.Description, true));
                        return new InteractionResult(InteractionEffect.ItemUsedUp, i, "The lead fits snugly into the input socket on the amp.");
                    }

                    if (Guitar.Name.EqualsIdentifier(i.Identifier))
                    {
                        if (obj.ContainsItem(Lead.Name))
                        {
                            easternHallway[Direction.East].Unlock();

                            if (lounge.FindCharacter(Beth.Name, out var b))
                            {
                                lounge.RemoveCharacter(b);
                                return new InteractionResult(InteractionEffect.NoEffect, i, "The guitar plugs in with a satisfying click. You play some punk and the amp sings. Beth's had enough! She bolts for the front door leaving it wide open! You are free to leave the flat! You unplug the guitar.");
                            }

                            return new InteractionResult(InteractionEffect.NoEffect, i, "The guitar plugs in with a satisfying click. You play some punk and the amp sings.");
                        }

                        return new InteractionResult(InteractionEffect.NoEffect, i, "You have no lead so you can't use the guitar with the amp...");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, i);
                }

                return new InteractionResult(InteractionEffect.NoEffect, i);
            };

            var regionMaker = new RegionMaker(Name, Description)
            {
                [2, 0, 0] = Bedroom.Create(),
                [2, 1, 0] = easternHallway,
                [1, 1, 0] = WesternHallway.Create(),
                [1, 2, 0] = Bathroom.Create(),
                [1, 3, 0] = roof,
                [1, 0, 0] = spareBedroom,
                [0, 1, 0] = Kitchen.Create(),
                [0, 0, 0] = lounge,
                [3, 1, 0] = Stairway.Create(),
                [2, 0, 1] = Attic.Create()
            };

            return regionMaker.Make(2, 0, 0);
        }

        #endregion
    }
}
