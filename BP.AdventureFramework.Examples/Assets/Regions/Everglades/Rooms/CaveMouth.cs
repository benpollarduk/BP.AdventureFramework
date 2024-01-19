﻿using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utilities.Templates;

namespace BP.AdventureFramework.Examples.Assets.Regions.Everglades.Rooms
{
    internal class CaveMouth : RoomTemplate
    {
        #region Constants

        private const string Name = "Cave Mouth";
        private const string Description = "A cave mouth looms in front of you to the north. You can hear the sound of the ocean coming from the west.";

        #endregion

        #region Overrides of RoomTemplate

        /// <summary>
        /// Instantiate a new instance of the room.
        /// </summary>
        /// <returns>The room.</returns>
        public override Room Instantiate()
        {
            return new Room(Name, Description, new Exit(Direction.North), new Exit(Direction.South), new Exit(Direction.West));
        }

        #endregion
    }
}
