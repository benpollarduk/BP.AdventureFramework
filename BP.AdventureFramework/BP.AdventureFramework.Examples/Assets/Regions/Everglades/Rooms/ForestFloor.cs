using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class ForestFloor : RoomTemplate<ForestFloor>
    {
        #region Constants

        private const string Name = "Forest Floor";
        private const string Description = "The forest is dense, with a few patches of light breaking the darkness. To the north is what looks like a small cave, to the south is the entrance to the forest.";

        #endregion

        #region Overrides of RoomTemplate<ForestFloor>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South));
        }

        #endregion
    }
}
