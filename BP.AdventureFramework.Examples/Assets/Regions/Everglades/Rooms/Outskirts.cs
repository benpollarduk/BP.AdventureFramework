using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class Outskirts : RoomTemplate
    {
        #region Constants

        private const string Name = "Outskirts";
        private const string Description = "A vast chasm falls away before you.";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.South));
        }

        #endregion
    }
}
