using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Examples.Assets.Regions.Everglades.Items;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class GreatWesternOcean : RoomTemplate<GreatWesternOcean>
    {
        #region Constants

        private const string Name = "Great Western Ocean";
        private const string Description = "The Great Western Ocean stretches to the horizon. The shore runs to the north and south. You can hear the lobstosities clicking hungrily. To the east is a small clearing.";

        #endregion

        #region Overrides of RoomTemplate<GreatWesternOcean>

        /// <summary>
        /// Create a new instance of the room.
        /// </summary>
        /// <param name="pC">The playable character.</param>
        /// <returns>The room.</returns>
        protected override Room OnCreate(PlayableCharacter pC)
        {
            var room = new Room(Name, Description, new Exit(Direction.East));
            room.AddItem(ConchShell.Create());
            return room;
        }

        #endregion
    }
}
