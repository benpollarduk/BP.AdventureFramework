using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities;


namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class Outskirts : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Outskirts";
        private const string Description = "A vast chasm falls away before you.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.South));
        }

        #endregion
    }
}
