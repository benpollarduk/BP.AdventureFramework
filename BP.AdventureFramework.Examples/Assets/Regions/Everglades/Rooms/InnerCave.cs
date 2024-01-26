using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Interaction;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Items;
using BP.AdventureFramework.Examples.Assets.Regions.Everglades.Items;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class InnerCave : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Inner Cave";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, string.Empty, new Exit(Direction.West), new Exit(Direction.North, true));

            InteractionCallback innerCaveInteraction = item =>
            {
                if (item != null && ConchShell.Name.EqualsExaminable(item))
                {
                    room[Direction.North].Unlock();
                    return new InteractionResult(InteractionEffect.ItemUsedUp, item, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces.");
                }

                if (item != null && Knife.Name.EqualsExaminable(item))
                    return new InteractionResult(InteractionEffect.NoEffect, item, "You slash wildly at the bats, but there are too many. Don't aggravate them!");

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            room.Interaction = innerCaveInteraction;
            room.SpecifyConditionalDescription(new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way.", () => !room[Direction.North].IsLocked));

            return room;

        }

        #endregion
    }
}
