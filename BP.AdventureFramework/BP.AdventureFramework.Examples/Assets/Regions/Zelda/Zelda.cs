using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda
{
    internal class Zelda : RegionTemplate<Zelda>
    {
        #region Constants

        private const string Name = "Kokiri Forest";
        private const string Description = "The home of the Kokiri tree folk.";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if the game has completed.
        /// </summary>
        /// <param name="game">The Game to check for completion.</param>
        /// <returns>The result of the completion check.</returns>
        public static CompletionCheckResult DetermineIfGameHasCompleted(Game game)
        {
            var atDestination = TailCave.Name.EqualsExaminable(game.Overworld.CurrentRegion.CurrentRoom);

            if (!atDestination)
                return CompletionCheckResult.NotComplete;

            return new CompletionCheckResult(true, "Game Over", "You have reached the end of the game, thanks for playing!");
        }

        #endregion

        #region Overrides of RegionTemplate<Zelda>

        /// <summary>
        /// Create a new instance of the region.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The region.</returns>
        protected override Region OnCreate(PlayableCharacter pC)
        {
            var regionMaker = new RegionMaker(Name, Description)
            {
                [0, 0, 0] = LinksHouse.Create(pC),
                [0, 1, 0] = OutsideLinksHouse.Create(pC),
                [1, 1, 0] = TailCave.Create(pC),
                [0, 2, 0] = Stream.Create(pC)
            };

            return regionMaker.Make(0, 0, 0);
        }

        #endregion
    }
}
