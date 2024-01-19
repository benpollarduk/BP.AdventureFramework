using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Flat.Rooms
{
    internal class Attic : RoomTemplate
    {
        #region Constants

        private const string Name = "Attic";
        private const string Description = "You are in the Attic. Even though there aren't many boxes up here the lack of light makes it pretty creepy.";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.Down));
        }

        #endregion
    }
}
