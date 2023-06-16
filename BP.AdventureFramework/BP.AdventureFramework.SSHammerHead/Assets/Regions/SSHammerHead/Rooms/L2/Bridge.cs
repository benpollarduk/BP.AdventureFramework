using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L2
{
    internal class Bridge : RoomTemplate<Bridge>
    {
        #region Constants

        private const string Name = "Bridge";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<Bridge>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.West), new Exit(Direction.East), new Exit(Direction.South));
        }

        #endregion
    }
}
