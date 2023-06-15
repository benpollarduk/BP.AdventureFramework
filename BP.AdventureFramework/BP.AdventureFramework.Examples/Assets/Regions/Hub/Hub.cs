using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Commands;
using BP.AdventureFramework.Examples.Assets.Regions.Hub.Rooms;
using BP.AdventureFramework.Interpretation;
using BP.AdventureFramework.Utilities;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Hub
{
    internal class Hub : RegionTemplate<Hub>
    {
        #region Overrides of RegionTemplate<Hub>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker("Jungle", "A dense jungle, somewhere tropical.");

            regionMaker[0, 0, 0] = Clearing.Create(pC);

            return regionMaker.Make(0, 0, 0);
        }

        #endregion

        #region StaticMethods

        internal static Region GenerateHub(Region[] otherRegions, Overworld overworld)
        {
            var region = Create(null);
            var room = region.CurrentRoom;

            foreach (var otherRegion in otherRegions)
            {
                room.AddItem(new Item($"{otherRegion.Identifier.Name} Sphere", "A glass sphere, about the size of a snooker ball. Inside you can see a swirling mist.", true)
                {
                    Commands = new[]
                    {
                        new CustomCommand(new CommandHelp($"Warp {otherRegion.Identifier.Name}", $"Use the {otherRegion.Identifier.Name} Sphere to warp to the {otherRegion.Identifier.Name}."), true, (g, a) =>
                        {
                            var move = overworld?.Move(otherRegion) ?? false;

                            if (!move)
                                return new Reaction(ReactionResult.Error, $"Could not move to {otherRegion.Identifier.Name}.");

                            g.DisplayTransition(string.Empty, $"You peer inside the sphere and feel faint. When the sensation passes you open you eyes and have been transported to the {otherRegion.Identifier.Name}.");

                            return new Reaction(ReactionResult.Internal, string.Empty);
                        })
                    }
                });
            }

            return region;
        }

        #endregion
    }
}
