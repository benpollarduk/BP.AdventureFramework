using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.SSHammerHead.Assets.Regions.SSHammerHead.Rooms.L1
{
    internal class Booster : RoomTemplate<Booster>
    {
        #region Constants

        private const string Name = "Booster";
        private const string Description = "";

        #endregion

        #region Overrides of RoomTemplate<Booster>

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
