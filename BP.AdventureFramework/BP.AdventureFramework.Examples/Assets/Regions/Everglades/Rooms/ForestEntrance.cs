using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class ForestEntrance : RoomTemplate<ForestEntrance>
    {
        #region Constants

        private const string Name = "Forest Entrance";
        private const string Description = "You are standing on the edge of a beautiful forest. There is a parting in the trees to the north.";

        #endregion

        #region Overrides of RoomTemplate<ForestEntrance>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.North));
        }

        #endregion
    }
}
