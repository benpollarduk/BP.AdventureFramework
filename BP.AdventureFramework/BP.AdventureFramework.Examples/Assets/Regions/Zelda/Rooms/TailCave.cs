using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Zelda.Rooms
{
    internal class TailCave : RoomTemplate<TailCave>
    {
        #region Constants

        internal const string Name = "Tail Cave";
        private const string Description = "The cave is dark, and currently very empty. Quite shabby really, not like the cave on Koholint at all...";

        #endregion

        #region Overrides of RoomTemplate<TailCave>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        protected override Room OnCreate()
        {
            return new Room(Name, Description, new Exit(Direction.West));
        }

        #endregion
    }
}
