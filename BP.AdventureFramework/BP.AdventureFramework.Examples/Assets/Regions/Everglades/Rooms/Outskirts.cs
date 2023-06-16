using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class Outskirts : RoomTemplate<Outskirts>
    {
        #region Constants

        private const string Name = "Outskirts";
        private const string Description = "A vast chasm falls away before you.";

        #endregion

        #region Overrides of RoomTemplate<Outskirts>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.South));
        }

        #endregion
    }
}
