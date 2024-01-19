using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Stairway : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Stairway";
        private const string Description = "You are in the Stairway. It is dimly lit because the bulbs have blown again, to the north is a staircase leading down to the other flats. Fausto, your next door neighbour is standing naked at the bottom of the stairs. He looks pretty pissed off about all the noise, and doesn't look like he is going to let you past. To the west is the front door of the flat.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
