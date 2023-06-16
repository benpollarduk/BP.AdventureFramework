using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class Cave : RoomTemplate<Cave>
    {
        #region Constants

        private const string Name = "Cave";
        private const string Description = "The cave is so dark you struggling to see. A screeching noise is audible to the east.";

        #endregion

        #region Overrides of RoomTemplate<Cave>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.East), new Exit(Direction.South));
        }

        #endregion
    }
}
